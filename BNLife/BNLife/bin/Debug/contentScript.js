//list for all divisions
var divs ;

//global index for post
var i=0;

//find all divisions of any page
function find_divs()
{ 
 divs= document.querySelectorAll('div');
}

//find next post
function next() {

//remove highlight from this division
  for (; i > 0; i--) {
   var flag =divs[i].id.indexOf("hyperfeed_story_id_");
   var flag2 =divs[i].id.indexOf("tl_unit_");
   if(flag>=0||flag2>=0)
   {
     var s = "#"+divs[i].id;
     $(s).css("box-shadow","");
     $(s).css("zoom","");
     $(s).css("filter","");
     $(s).css("opacity","");
     break;
   }
  }

//highlight the next division
  i++;
for (; i < divs.length; i++) {
   var flag =divs[i].id.indexOf("hyperfeed_story_id_");
   var flag2 =divs[i].id.indexOf("tl_unit_");
   if(flag>=0||flag2>=0)
   {
     var s = "#"+divs[i].id;
    $(s).css("box-shadow","3px 3px 15px #666");
     $(s).css("zoom","1");
     $(s).css("filter","alpha(opacity=100)");
     $(s).css("opacity","1");
     break;
   }
  }
}

// find previous post
function pre() {
//remove highlight from this post
for (; i < divs.length; i++) {
   var flag =divs[i].id.indexOf("hyperfeed_story_id_");
   var flag2 =divs[i].id.indexOf("tl_unit_");
   if(flag>=0||flag2>=0)
   {
     var s = "#"+divs[i].id;
     $(s).css("box-shadow","");
     $(s).css("zoom","");
     $(s).css("filter","");
     $(s).css("opacity","");
     break;
   }
  }
//add highlight for the previous post
  i--;
  for (; i > 0; i--)  {
   var flag =divs[i].id.indexOf("hyperfeed_story_id_");
   var flag2 =divs[i].id.indexOf("tl_unit_");
   if(flag>=0||flag2>=0)
   {
     var s = "#"+divs[i].id;
     $(s).css("box-shadow","3px 3px 15px #666");
     $(s).css("zoom","1");
     $(s).css("filter","alpha(opacity=100)");
     $(s).css("opacity","1");
     break;
   }
  }
}

// go to profile picture
function goToProfile(){
    
    var element = document.getElementsByTagName("img");
    for (var i = 0; i < element.length; i++) {
        var ele = element[i].id.indexOf("profile_pic_header_");
        if(ele>=0)
        {
          var id_ele = "#" +element[i].id;
          $(id_ele).trigger("click");
          break;
        }
    };

}

//login to facebook account
function login(name,pass){

$("#loginform #email").val(name);
$("#loginform #pass").val(pass);
$("#loginform #loginbutton").trigger("click");

}






