var file = null;
$(document).ready(function () {
    $("#datepicker").datetimepicker({ format: 'd-m-Y', lang: 'vi', timepicker: false });
    $("#timepicker").datetimepicker({
        datepicker: false,
        format: 'H:i'
    });
    $('#uploadFile').on('change', function (e) {
        var file = e.target.files[0];
    });
});
function updateComment() {
    var addCommentCrm = new {
        Id: window.Id, CrmStatusId: $("#CrmStatusId"), Comment: $("#Comment"), File: file
    };
    $.post('/Crm/UpdateComment',
        { model: addCommentCrm },
        function (data) {
            if (data) {
                $("#Comment").text('');
                $("#uploadFile").value = "";
                loadComment();
            }
        });
}
function loadComment() {
    $.post('/Crm/LoadComments',
        { model: addCommentCrm },
        function (data) {
            if (data) {
                console.log(data);
            }
        });
}