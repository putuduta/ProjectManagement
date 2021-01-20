function myFunction() {

    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("ProjectList");
    tr = table.getElementsByTagName("tr");


    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByClassName("ProjectName")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

$('.group').on('change', function () {
    var totalSeen = $("input.group:checked").length;
    if ($(this).prop("checked")) {
        var id = $(this).val();
        $("#formAddSecur").submit();

    }
});

$('#formAddSecur').submit((e) => {
    e.preventDefault();
    var data = $('#formAddSecur').serialize();
    console.log(data);
    $.ajax({
        type: "POST",
        url: "Home/StoreAuthUser",
        data: data,
        success: (data) => {
            console.log("sukses");
            if (data.success) {
                alert("Success Add Authorized User")
            } else {
                alert("Cannot Add Authorized User")
            }
        },
        error: (err) => {
            alert(err.status);
        }

    });
});

console.log(status);

$(".addProject").click(function () {
    var id = $(this).data('id');
    console.log(id);
    $('#formAddSecur #ProjectID').val(id);
});


function AddProject() {
    var data = $('#formAddProject').serialize();
    $.ajax({
        type: "POST",
        url: "Home/Store",
        data: data,
        success: (data) => {
            if (data.success) {
                alert("Success Add Project")
                $('#ProjectList').load('/Home #ProjectList');
            } else {
                alert("Cannot Add Project")
            }
        }

    });
}


$('#formAddProject').validate({
    rules: {
        ProjectName: {
            required: true,
        }
    }
});

$(document).ready(function () {
    $(".link").click(function (event) {
        var $this = $(this),
            url = $this.val();
        window.location.href = url;
    });
});