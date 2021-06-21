
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/FootballersList")
    .build();

hubConnection.start();

hubConnection.on("UpdatePage", function () {
    loadList();
});

loadList();

function loadList() {
    $.ajax({
        url: '/Footballers/GetFootballersTableContent',
        method: 'GET',
        success: (result) => {
            $("#tableBody").html(result);
        },
        error: (error) => {
            console.log(error)
        }
    });
}