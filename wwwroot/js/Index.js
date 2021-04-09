// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            url: `/Home/AddMember?memberName=${newcomerName}`,
            success: function (data) {
                // Remember string interpolation
                $("#list").append(`<li class="member" member-id="${data}">
		            <span class="name">${newcomerName}</span><i class="delete fa fa-remove"></i><i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
		        </li>`);

                $("#newcomer").val("");
            },
            error: function (data) {
                alert(`Failed to add ${newcomerName}`);
            },
        });
    })

    $("#clear").click(function () {
        $("#newcomer").val("");
    })

    // Bind event to dynamically created element: https://makitweb.com/attach-event-to-dynamically-created-elements-with-jquery
    $("#list").on("click", ".delete", function () {
        var targetMemberTag = $(this).closest('li');
        var id = targetMemberTag.attr('member-id');
        $.ajax({
            url: `/Home/RemoveMember/${id}`,
            type: 'DELETE',
            success: function () {
                targetMemberTag.remove();
            },
            error: function () {
                alert(`Failed to delete member with id=${id}`);
            }
        })
    })

    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var id = targetMemberTag.attr('member-id');
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("member-id", id);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        let name = $("#classmateName").val();
        let id = $('#editClassmate').attr("member-id");
        let data = [
            {
                "op": "add",
                "path": "/name",
                "value": name
            }
        ];

        $.ajax({
            url: `/Home/Update/${id}`,
            type: 'PATCH',
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function () {
                console.log('Rename successful')
            },
            error: function () {
                alert(`Failed to update member with id=${id}`);
            }
        })
        console.log('submit changes to server');
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })
});