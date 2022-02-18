    $(document).ready(function () {
        //refresh folder
        function refresh() {
            let pn = $("#projectsSection").data("pageid");
            console.log("To jest pageActive:" + pn);
            $.post("/Projects/UpdateProjects", { page: pn }, function (data) {
                $("#projectsSection").html(data);
            });
        }
         //active status
        $("body").on("click", ".removeButton", function (event) {
            event.preventDefault();
            let toHide = $(this).closest(".project-item");
            let send = $(this).data("id");
            $.post("/Projects/ActiveStatusJS", { id: send }, function (data) {
                if (data == true) {
                    toHide.fadeOut();
                    refresh();
                }
                else {
                    alert("Wystąpił problem..");
                }
            });
        });

        //kill project
        $("body").on("click", ".killButton", function (event) {
            event.preventDefault();
            let toHide = $(this).closest(".project-item");
            let send = $(this).data("id");
            $.post("/Projects/KillJS", { id: send }, function (data) {
                if (data == true) {
                    toHide.fadeOut();
                    refresh();
                }
                else {
                    alert("Wystąpił problem..");
                }
            });
        });

        //hide text
        let hideEmenet = $(".hideElement");
        hideEmenet.hide();
        $(".hideButton").on("click", function () {
                hideEmenet.slideToggle();
        });

    });