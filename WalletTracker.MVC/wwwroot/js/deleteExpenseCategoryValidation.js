// Validate form responsible for deleting expense category
$("#deleteExpenseCategoryForm").validate({
    rules: {
        Id: {
            required: true,
            min: 1
        }
    },
    messages: {
        Id: {
            min: "This field is required."
        }
    },
    errorPlacement: function (error, element) {
        if ($(element).attr("name") == "Id")
            $("#idSpan").text(error.text());
    },
    success: function (label, element) {
        if ($(element).attr("name") == "Id")
            $("#idSpan").text("");
    }
});