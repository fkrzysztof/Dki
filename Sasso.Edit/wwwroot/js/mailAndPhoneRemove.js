$(document).ready(function () {

    $("body").on("click", "#addPhone", function (e) {
        e.preventDefault();
        $(".phone:first").clone().find("input:text").val("").end().appendTo("#phoneRow");
    });

    $("body").on("click", "#addEmail", function (e) {
        e.preventDefault();

        $(".email:first").clone().find("input:text").val("").end().appendTo("#emailRow");
    });

    $("body").on("click", ".cutEmail", function (e) {
        e.preventDefault();
        if ($(".email").length > 1)
            $(this).closest("div.email").remove();
        else
            $(".emailAddress").val("");
    });

    $("body").on("click", ".cutPhone", function (e) {
        e.preventDefault();
        if ($(".phone").length > 1)
            $(this).closest(".phone").remove();
        else {
            $(".phoneNameAddress").val("");
            $(".phoneNumberAddress").val("");
        }
    });
});