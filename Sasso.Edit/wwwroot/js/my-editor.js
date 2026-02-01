document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('textarea').forEach(ta => {
        tinymce.init({
            target: ta,
            menubar: false,
            plugins: 'lists link',
            toolbar: 'undo redo | bold italic | bullist numlist | link',
            height: Math.max(400, ta.rows * 20),
            content_css: false
        });
    });
});
