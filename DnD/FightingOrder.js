var data = [];
var enemyNum = 1;

$("#add_btn").on('click', function(e) {
  e.preventDefault();
  var initiative = $('#id_initiative').val();
  if ($('#id_name').val() === "Enemy"){
	var name = $('#id_name').val() + enemyNum++;}
  else var name = $('#id_name').val();
  var hp = $('#id_hp').val();
  
  $('#id_initiative').val('');
  $('#id_hp').val('');

  var array = {
    "initiative" : initiative,
    "name": name,
    "hp": hp,
	"max": hp
  };

  data.push(array);

  data.sort(function(a, b) {
    return a.initiative - b.initiative;
  });

  $('.mytab').html('');
  $.each(data, function(index, value) {
	  if(value.name.includes("Enemy")){
    $('<tr class="badChar"><td>' + value.initiative + '</td><td>' + value.name + '</td><td>' + value.hp + '/' + value.max + 
	'</td><td><input type="button" class="damage_btn" id="damage_btn">Damage</button></td>' + 
	'<td><input type="button" class="heal_btn" id="heal_btn">Heal</button></td><td>' +
	'<input type="button" class="delete_btn" id="delete_btn" value="Remove"></input></td></tr>').appendTo($('.mytab'));
	  }
	  else{
	$('<tr class="goodChar"><td>' + value.initiative + '</td><td>' + value.name + '</td><td>' + value.hp + '/' + value.max + 
	'</td><td><input type="button" class="damage_btn" id="damage_btn">Damage</button></td>' + 
	'<td><input type="button" class="heal_btn" id="heal_btn">Heal</button></td><td>' +
	'<input type="button" class="delete_btn" id="delete_btn" value="Remove"></input></td></tr>').appendTo($('.mytab'));
	  }
  });

});

$("#charTable").on('click', 'input[id="damage_btn"]', function(event) {
var row_index = $(this).parent().parent().index();
var currHP = parseInt(data[row_index].hp, 10) - $('#damageIn').val();
if(currHP < 0) currHP = 0;
data[row_index].hp = currHP;
$('.mytab').html('');
  $.each(data, function(index, value) {
    if(value.name.includes("Enemy")){
    $('<tr class="badChar"><td>' + value.initiative + '</td><td>' + value.name + '</td><td>' + value.hp + '/' + value.max + 
	'</td><td><input type="button" class="damage_btn" id="damage_btn">Damage</button></td>' + 
	'<td><input type="button" class="heal_btn" id="heal_btn">Heal</button></td><td>' +
	'<input type="button" class="delete_btn" id="delete_btn" value="Remove"></input></td></tr>').appendTo($('.mytab'));
	  }
	  else{
	$('<tr class="goodChar"><td>' + value.initiative + '</td><td>' + value.name + '</td><td>' + value.hp + '/' + value.max + 
	'</td><td><input type="button" class="damage_btn" id="damage_btn">Damage</button></td>' + 
	'<td><input type="button" class="heal_btn" id="heal_btn">Heal</button></td><td>' +
	'<input type="button" class="delete_btn" id="delete_btn" value="Remove"></input></td></tr>').appendTo($('.mytab'));
	  }
  });

});

$("#charTable").on('click', 'input[id="heal_btn"]', function(event) {
var row_index = $(this).parent().parent().index();
var currHP = parseInt(data[row_index].hp, 10) + parseInt($('#healingIn').val(), 10);
if(currHP > parseInt(data[row_index].max, 10)) currHP = data[row_index].max;
data[row_index].hp = currHP;
$('.mytab').html('');
  $.each(data, function(index, value) {
    if(value.name.includes("Enemy")){
    $('<tr class="badChar"><td>' + value.initiative + '</td><td>' + value.name + '</td><td>' + value.hp + '/' + value.max + 
	'</td><td><input type="button" class="damage_btn" id="damage_btn">Damage</button></td>' + 
	'<td><input type="button" class="heal_btn" id="heal_btn">Heal</button></td><td>' +
	'<input type="button" class="delete_btn" id="delete_btn" value="Remove"></input></td></tr>').appendTo($('.mytab'));
	  }
	  else{
	$('<tr class="goodChar"><td>' + value.initiative + '</td><td>' + value.name + '</td><td>' + value.hp + '/' + value.max + 
	'</td><td><input type="button" class="damage_btn" id="damage_btn">Damage</button></td>' + 
	'<td><input type="button" class="heal_btn" id="heal_btn">Heal</button></td><td>' +
	'<input type="button" class="delete_btn" id="delete_btn" value="Remove"></input></td></tr>').appendTo($('.mytab'));
	  }
  });

});

$("#charTable").on('click', 'input[id="delete_btn"]', function(event) {
var row_index = $(this).parent().parent().index();
data.splice(row_index, 1);
$(this).parent().parent().remove();
});