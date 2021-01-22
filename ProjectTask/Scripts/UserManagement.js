$('#formAddUser').validate({
    rules: {
        Username: {
            required: true
        },
        UserPassword: {
            required: true,
            alphanumeric: true
        },
        ImageUpload: {
            required: true,
            extension: "jpeg|gif|png"
        }
    }
});


$("#mySearch").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#List tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});





$('.fileUpload-2').on('change', function () {
    //get the file name
    var fileName = $(this).val();
    //replace the "Choose a file" label
    $(this).next('.custom-file-label').html(fileName);
})

$('.fileUpload-1').on('change', function () {
    //get the file name
    var fileName = $(this).val();
    //replace the "Choose a file" label
    $(this).next('.custom-file-label').html(fileName);
})


function AjaxPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Home/AddUser",
        data: new FormData(formData),
        success: function (result) {
            alert("Success Add User");
            $('#tableList').load('/Home/UserManagement #tableList');
            $("#add-user .closeModal").click();
        },
        error: function (err) {
            alert("Error Add User");
            console.log(err);
        }
    }
    if ($(formData).attr('enctype') == "multipart/form-data") {
        ajaxConfig["contentType"] = false;
        ajaxConfig["processData"] = false;
    }

    $.ajax(ajaxConfig);
    return false;
}

console.log(status);

$(".addUser").click(function () {

    $('#UsernameInput').empty();
    $('#PasswordInput').empty();
    $('#RolesInput').empty();
    $('#PhotoInput').empty();
    $('#StatusInput').empty();

    var id = $(this).data('id');
    console.log("masuk");
    console.log(id);
    $('.modal-body #UserID').val(id);

    $.ajax({
        type: "GET",
        url: "/Home/GetOneUser",
        data: {
            id: id
        },
        success: function (result) {
            $('#UsernameInput').append(`<input type="text" class="form-control mt-2" id="EditUsername" name="Username" placeholder="${result.Username}" value="${result.Username}" readonly>`);
            $('#PasswordInput').append(`<input type="password" class="form-control mt-2" id="EditUserPassword" name="UserPassword" value="${result.UserPassword}" required>`);

            if (result.UserRoles === "Admin") {
                $('#RolesInput').append(
                    `<select id="UserRoles" name="UserRoles" class="form-control mt-2">
                                        <option  value="Admin" selected>Admin</option>
                                        <option value="User">User</option>
                                    </select>`
                );
            } else if (result.UserRoles === "User") {
                $('#RolesInput').append(
                    `<select id="UserRoles" name="UserRoles" class="form-control mt-2">
                                        <option value="User" selected >User</option>
                                        <option value="Admin">Admin</option>
                                    </select>`
                );
            }

            $('#PhotoInput').append(
                `<img src="/AppFile/Images/${result.UserPhoto}" class="rounded-circle img-size" alt="No Images">
                                        <input type="text" id="UserPhoto" name="UserPhoto" value="${result.UserPhoto}" hidden>`
            );



            if (result.UserStatus === "Active") {
                $('#StatusInput').append(
                    `<input type="checkbox" class="custom-control-input" id="UserStatus" name="UserStatus" value="Active" checked>
                                     <label class="custom-control-label" for="UserStatus">Active</label>`
                );
            } else {
                $('#StatusInput').append(
                    `<input type="checkbox" class="custom-control-input" id="UserStatus" name="UserStatus" value="Active">
                                     <label class="custom-control-label" for="UserStatus">Active</label>`
                );
            }


        },
        error: function (err) {
            console.log(err);
        }
    })
});

function EditPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Home/EditUser",
        data: new FormData(formData),
        success: function (result) {
            alert("Success Edit User");
            
            $('#tableList').load('/Home/UserManagement #tableList');
            $("#exampleEdit .closeModal").click();
        },
        error: function (err) {
            alert("Error Edit User");
            console.log(err);
        }
    }
    if ($(formData).attr('enctype') == "multipart/form-data") {
        ajaxConfig["contentType"] = false;
        ajaxConfig["processData"] = false;
    }

    $.ajax(ajaxConfig);
    return false;
}

$(document).ready(function () {
    $(".link").click(function (event) {
        var $this = $(this),
            url = $this.val();
        window.location.href = url;
    });
});