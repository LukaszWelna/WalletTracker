// Fill the form after selecting category
$(function () {
    $("#Id").on("change", function () {
        var selectedText = $("#Id option:selected").text();
        var selectedId = $(this).val();

        if (selectedId == 0) {
            selectedText = "";
            $("#LimitIsActive").prop("checked", false);
            $("#Limit").val('');
        } else {
            $("#Name").val(selectedText);

            $.ajax({
                url: `/Settings/GetExpenseCategoryById/${selectedId}`,
                type: "get",
                success: function (data) {
                    $("#LimitIsActive").prop("checked", data["limitIsActive"]);
                    let limit = String(data["limit"]);
                    $("#Limit").val(limit.replace(/\./g, ','));
                },
                error: function () {
                    toastr["error"]("Something went wrong");
                }
            });
        }
    });
});

// Validate form responsible for editing expense category
$("#editExpenseCategoryForm").validate({
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
                url: "/Settings/CheckExpenseCategoryNameExists",
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
        },
        Limit: {
            limitMin: true,
            amountMax: true,
            decimalPlaces: true
        },
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
        else if ($(element).attr("name") == "Limit")
            $("#limitSpan").text(error.text());
    },
    success: function (label, element) {
        if ($(element).attr("name") == "Id")
            $("#idSpan").text("");
        else if ($(element).attr("name") == "Name")
            $("#nameSpan").text("");
        else if ($(element).attr("name") == "Limit")
            $("#limitSpan").text("");
    }
});