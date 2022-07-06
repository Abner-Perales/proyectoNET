/*
window.addEventListener('keypress', function (event) {
    if (event.key == 'Enter') {
        validateInput(event.path[2].title);
    }
}, false);
*/
var input = document.getElementById('inputId');

input.addEventListener('keypress', function (event) {
    if (event.key == 'Enter') {
        //console.log(event);
        validateInput(event.path[7].title);
    }
}, false);

function validateInput(title) {
    var inputId = document.getElementById('inputId').value;
    if (inputId != '') {
        //console.log(inputId);
        //console.log(title);
        var modalId = new bootstrap.Modal(document.getElementById('modalId'), {});
        var modalTitle = document.getElementById('modalTitle');
        var modalBody = document.getElementById('modalBody');

        if (title == 'MemberIn') {
            modalTitle.innerHTML = 'Check in';
            modalBody.innerHTML = 'Registered entry for the member ' + inputId + ' (' + moment(new Date()).format("HH:mm:ss") + ')';
            modalId.show();
        }
        else if (title == 'MemberOut') {
            modalTitle.innerHTML = 'Check out';
            modalBody.innerHTML = 'Registered output for the member ' + inputId + ' (' + moment(new Date()).format("HH:mm:ss") + ')';
            modalId.show();
        }
        setTimeout(function () { modalId.hide(); }, 3000);
    }
}
