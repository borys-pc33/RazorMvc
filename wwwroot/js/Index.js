﻿// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();
        let data = {
            "name": newcomerName
        };

        $.ajax({
            url: `/Intern`,
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (resultData) {
                $("#newcomer").val("");
            },
            error: function (data) {
                alert(`Failed to add ${newcomerName}`);
            },
        });
    })

    function createNewcomer(name, id) {
        // Remember string interpolation
        $("#list").append(`<li class="member" member-id="${id}">
		            <span class="name">${name}</span><i class="delete fa fa-remove"></i><i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
		        </li>`);
    }
    $("#clear").click(function () {
        $("#newcomer").val("");
    })

    // Bind event to dynamically created element: https://makitweb.com/attach-event-to-dynamically-created-elements-with-jquery
    $("#list").on("click", ".delete", function () {
        var targetMemberTag = $(this).closest('li');
        var id = targetMemberTag.attr('member-id');
        $.ajax({
            url: `/Intern/${id}`,
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
        let data = {
            "name": name
        };

        $.ajax({
            url: `/Intern/${id}`,
            type: 'PUT',
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function () {
                $(`.member[member-id=${id}]`).find(".name").text(name);
            },
            error: function () {
                alert(`Failed to update member with id=${id}`);
            }
        })
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })

    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/messagehub")
        .configureLogging(signalR.LogLevel.Information)
        .withAutomaticReconnect()
        .build();

    connection.on("AddMember", function (name, id) {
        createNewcomer(name, id);
    });

    connection.start().then(function () {
        console.assert(connection.state === signalR.HubConnectionState.Connected);
        console.log('Connection started.')
    }).catch(function (err) {
        return console.error(err.toString());
    });
});