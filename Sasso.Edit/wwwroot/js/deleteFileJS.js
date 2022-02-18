$(document).ready(function () {
    $("body").on("click", ".removeFile", function (e) {
        e.preventDefault();
        let toHide = $(this).closest(".hide-show-item-file");
        let send = $(this).data("id");
        $.post("/Projects/DeleteFileJS", { id: send }, function (data) {
            if (data == true) {
                toHide.fadeOut();
            }
            else {
                alert("Wystąpił problem..");
            }
        });
    });
});