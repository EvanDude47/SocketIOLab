var io = require('socket.io')(process.envPort||3000);
var shortid = require('shortid');

console.log("Server Started");
//console.log(shortid.generate());

var players = [];

io.on('connection', function(socket){
    var thisPlayerId = shortid.generate();
    
    players.push(thisPlayerId);

    console.log("Client Connected spawning player id: " + thisPlayerId);
    
    socket.broadcast.emit('spawn player', {id:thisPlayerId});

    players.forEach(function(playerId){
        if(playerId === thisPlayerId) return;

        socket.emit('spawn player', {id:playerId});
        console.log("Adding a new player" + playerId);
    });

    socket.on('playerhere', function(data){
        console.log("Player is logged in");
    });

    socket.on('disconnect', function(){
        console.log("Player Disconnected");
        players.splice(players.indexOf(thisPlayerId), 1);
        socket.broadcast.emit('disconnected', {id:thisPlayerId});
    });
});