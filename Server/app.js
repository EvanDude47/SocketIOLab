var io = require('socket.io')(process.envPort||3000);

console.log("Server Started");

var playerCount = 0;

io.on('connection', function(socket){
    console.log("Client Connected");
    
    socket.broadcast.emit('spawn player');
    playerCount++;

    for(var i = 0; i < playerCount; i++){
        socket.emit('spawn player');
        console.log("Adding a new player");
    }

    socket.on('playerhere', function(data){
        console.log("Player is logged in");
    });

    socket.on('disconnect', function(){
        console.log("Player Disconnected");
        playerCount--;
    });
});