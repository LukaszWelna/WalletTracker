// Fill the form after selecting category
$(function () {
    $("#Id").on("change", function () {
        var selectedText = $("#Id option:selected").text();
        if ($(this).val() == 0) {
            selectedText = "";
        }
        $("#Name").val(selectedText);
    })   
});

// Validate form responsible for adding new income category
$("#editIncomeCategoryForm").validate({
    rules: {
        Id: {
            required: true,
            min: 1
        },
        Name: {
            required: true,
            minlength: 2,
            maxlength: 25,
            remote: {
                url: "/Settings/CheckIncomeCategoryNameExists",
                type: "post",
                data: {
                    id: function () {
                        return $("#Id").val();
                    },
                    name: function () {
                        return $("#Name").val();
                    }
                }
            }
        }
    },
    messages: {
        Id: {
            min: "This field is required."
        },
        Name: {
            minlength: "This field must contain at least 2 characters.",
            maxlength: "This field contain less than 26 characters.",
            remote: "Name of category already exists in database."
        }
    },
    errorPlacement: function (error, element) {
        if ($(element).attr("name") == "Id")
            $("#idSpan").text(error.text());
        else if ($(element).attr("name") == "Name")
            $("#nameSpan").text(error.text());
    },
    success: function (label, element) {
        if ($(element).attr("name") == "Id")
            $("#idSpan").text("");
        else if ($(element).attr("name") == "Name")
            $("#nameSpan").text("");
    }
});