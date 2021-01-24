$('.class-0').addClass("active");

function getWorkDate(id) {
    $.ajax({
        type: "GET",
        url: "/UserPage/GetOneWorkItem",
        data: {
            id: id
        },
        success: function (result) {

            $('.date-class').append(`
                        <p>${parseJsonDate(result.WorkItemStartDate)} - ${parseJsonDate(result.WorkItemEndDate)}</p>
                      `);
        },
        error: function (err) {
            console.log(err);
        }
    })
}

if ($(".class-0")) {
    $('.date-class').empty()
    var id = $('#buat-awal-0').val();
    getWorkDate(id);
}

$('.workName').click(function () {
    $('.date-class').empty();
    var id = $(this).data('id');

    getWorkDate(id);
});

function parseJsonDate(jsonDate) {

    if (jsonDate != null) {
        var fullDate = new Date(parseInt(jsonDate.substr(6)));
        var twoDigitMonth = (fullDate.getMonth() + 1) + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;

        var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
        var currentDate = twoDigitMonth + "/" + twoDigitDate + "/" + fullDate.getFullYear();

        return currentDate;
    }
    return jsonDate = " ";
};

$(".editWorkItems").click(function () {

    $('#itTitle').empty();
    $('#NameInput').empty();
    $('#ProjectInput').empty();
    $('#StateInput').empty();

    var id = $(this).data('id');
    console.log("masuk");
    console.log(id);
    $('.modal-body #WorkItemID').val(id);

    $.ajax({
        type: "GET",
        url: "/UserPage/GetOneWorkItem",
        data: {
            id: id
        },
        success: function (result) {
            $('#itTitle').append(`<h4 class="text-primary">${result.WorkItemName}</h4>`);
            $('#NameInput').append(`<input type="text" class="form-control mt-2" id="WorkItemName" name="WorkItemName"
                        placeholder="${result.WorkItemName}" value="${result.WorkItemName}" readonly>`);
            $('#ProjectInput').append(`<input type="hidden" class="form-control mt-2" id="ProjectID" name="ProjectID"
                        placeholder="${result.ProjectID}" value="${result.ProjectID}">`);
            $('#StateInput').append(`<input type="hidden" class="form-control mt-2" id="WorkItemState" name="WorkItemState"
                        placeholder="${result.WorkItemState}" value="${result.WorkItemState}">`);
        },
        error: function (err) {
            console.log(err);
        }
    })
});

$(".addWorkItems").click(function () {

    var id = $(this).data('id');
    console.log("masuk");
    console.log(id);
    $('.modal-body #ProjectID').val(id);
});

function AddWork(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/UserPage/Store",
        data: new FormData(formData),
        success: function (result) {
            alert("Success Add Work Item");

            location.reload();
            $("#exampleAdd .closeModal").click();
        },
        error: function (err) {
            alert("Error Add Work Item");
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

function EditWork(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/UserPage/EditWorkItem",
        data: new FormData(formData),
        success: function (result) {
            alert("Success Edit Work Item");

            location.reload();
            $("#exampleEdit .closeModal").click();
        },
        error: function (err) {
            alert("Error Edit Work Item");
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

