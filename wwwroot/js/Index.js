// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            url: `/Home/AddMember?member=${newcomerName}`,
            success: function (data) {
                // Remember string interpolation
                $("#list").append(`<li class="member">
		            <span class="name">${data}</span><span class="delete fa fa-remove"></span><i class="fa fa-pencil"></i>
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

    $(".delete").click(function () {
        var targetMemberTag = $(this).parent('li');
        var index = targetMemberTag.index(targetMemberTag.parent());
        $.ajax({
            url: `/Home/RemoveMember/${index}`,
            type: 'DELETE',
            success: function () {
                targetMemberTag.remove();
            },
            error: function () {
                alert(`Failed to delete member with index=${index}`);
            }
        })
    })
});