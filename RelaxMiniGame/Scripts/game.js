var gameContainer = document.querySelector('.game-container');
var gameReset = document.getElementById('game-reset');
var gameLevel = document.getElementById('game-level');
var counter = 0;
var iLevel = 5;
var iWin = 1;

gameReset.addEventListener('click', resetGame, false);

function Tile(row, column) {
    this.row = row;
    this.column = column;
}

function playGame() {
    $('.game-cell').removeClass('is-active').html('');

    var tiles = [];
    var ROWS = 8;
    var COLUMNS = 5;
    var i = 1;

    while (i <= iLevel) {
        var isFound = false;
        var rowRandom = getRandomInt(0, ROWS - 1);
        var columnRandom = getRandomInt(0, COLUMNS - 1);
        var tile = new Tile(rowRandom, columnRandom);

        if (tiles.length) {
            for (var ti = 0, tiLen = tiles.length; ti < tiLen; ti++) {
                if (JSON.stringify(tiles[ti]) === JSON.stringify(tile)) {
                    isFound = true;
                    break;
                }
            }
        }

        if (!isFound) {
            tiles.push(tile);

            updateTileGame(tile, i);

            i++;
        }
    }
}

function openTileGame() {
    var data = this.data;

    this.classList.add('is-active');

    counter++;

    if (data !== counter) {
        sweetAlert({
            title: 'Bạn đã thua. Quá gà!',
            imageUrl: '/Images/thumbs-down.png',
            animation: false,
            confirmButtonText: 'Chơi lại'
        }, function() {
            beginGame();
        });

        beginGame();
    } else {
        $(this).html(data);
    }

    if (counter === iLevel) {
        iWin++;
        gameLevel.innerHTML = iWin;

        sweetAlert({
            title: 'Bạn đã thắng, quá dữ!',
            text: 'Chơi tiếp level ' + iWin,
            imageUrl: '/Images/thumbs-up.png',
            animation: false,
            confirmButtonText: 'Chơi tiếp'
        }, function() {
            iLevel++;
            resetGame();
        });
    }
}

function updateTileGame(tile, ind) {
    var tileNumber = document.createElement('div');
    tileNumber.className = 'tile-number';
    tileNumber.innerHTML = ind;
    tileNumber.data = ind;
    
    $('.game-row').eq(tile.column).find('.game-cell').eq(tile.row).html(tileNumber);

    $('.game-row').find('.tile-number').parent('.game-cell').addClass('is-active');         

    tileNumber.addEventListener('click', openTileGame, false);
}

function updateImageGame() {
    var tileImage = document.createElement('div');
    tileImage.className = 'tile-image';

    $('.tile-number').html(tileImage);
}

function resetGame() {
    counter = 0;
    gameReset.classList.add('is-disabled');
    gameContainer.classList.add('is-visible');
    playGame();
    setTimeout(function () {
        updateImageGame();
        gameReset.classList.remove('is-disabled');
        gameContainer.classList.remove('is-visible');
    }, 5000);
}

function beginGame() {
    iLevel = 5;
    iWin = 1;
    gameLevel.innerHTML = iWin;
    resetGame();
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}