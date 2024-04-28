// Validate form responsible for adding new expense category
$("#addExpenseCategoryForm").validate({
    rules: {
        Name: {
            required: true,
            minlength: 2,
            maxlength: 25,
            remote: {
                url: "/Settings/CheckExpenseCategoryNameExists",
                type: "post",
                data: {
                    id: function () {
                        return 0;
                    },
                    name: function () {
                        return $("#Name").val();
                    }
                }
            }
        }
    },
    messages: {
        Name: {
            minlength: "This field must contain at least 2 characters.",
            maxlength: "This field contain less than 26 characters.",
            remote: "Name of category already exists in database."
        }
    },
    errorPlacement: function (error, element) {
        if ($(element).attr("name") == "Name")
            $("#nameSpan").text(error.text());
    },
    success: function (label, element) {
        if ($(element).attr("name") == "Name")
            $("#nameSpan").text("");
    }
});