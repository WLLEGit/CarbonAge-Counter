/* HUIL jieshuiguo game 版本:1.0*/
var cte; //画布
var cteWidth; //画布宽度
var cteHeight; //画布高度
var erval; //循环函数名
var speed = 1; //不变速度名
var bgW; //背景宽度
var bgH; //背景高度
var sgY = speed; //垃圾速度
var sgJg = 100; //垃圾间隔
var sgRed; //垃圾旋转
var sgGs = 0; //接到垃圾个数
var ps = 0; //游戏局数
var fs = 0; //分数
var vita = 10; //生命
var pic = 0; //帧数
var gamev = false; //游戏状态

var men = new Image();
var bg = new Image();
var sg1 = new Image();
var sg2 = new Image();
var sg3 = new Image();
var sg5 = new Image();
var pan = new Image();
var fsb = new Image();
var xue = new Image();
var zd = new Image();

function objGame(){
	this.x = 0;
	this.y = 0;
	this.image = null;
}
function Menn(){};
Menn.prototype = new objGame();
var menn = new Menn();
var pann = new Menn();
function Sg(){};
Sg.prototype = new objGame();
Sg.prototype.red = 0;
Sg.prototype.panS = false; //新建水果在盘上
Sg.prototype.hid = false; //新建水果是否隐藏
Sg.prototype.redC = 0;   //水果旋转角度常量
Sg.prototype.fs = 0;
Sg.prototype.panX = 0;
var sgArr = new Array();
var sgj = 0;
function Zd(){};
Zd.prototype = new objGame();
Zd.prototype.red = 0;
Zd.prototype.redC = 0;
Zd.prototype.hid = false;
var zdj = 0; //炸弹间隔
var zdJg = 800; //炸弹间隔
var zdY = speed;
var zdiX;  //炸弹图片的x轴
var zdArr = new Array();

//水果音效
function SgYx(){
	document.getElementById("sgmu").currentTime = 0;
	document.getElementById("sgmu").play();
}

//炸弹音效
function ZdYx(){
	document.getElementById("zdmu").currentTime = 0;
	document.getElementById("zdmu").play();
}

//绘制垃圾
function HzSg(){
	if(pic%sgJg==0){
		var sgg = new Sg();
		sgg.x = Math.random()*cteWidth;
		sgg.y = -20;
		sgg.redC = -(Math.random()*6-3);  //设置垃圾旋转角度
		//判断是可回收还是不可回收
		if(Math.random()*10>5){
			sgg.image = sg1;
			sgg.fs = 10;
		}
		else(Math.random()*10>6)
		{
			sgg.image = sg2;
			sgg.fs = 20;}
		if(Math.random *10>5){
				sgg.image = sg3;
				sgg.fs=10;
			}
		else if (Math.random()*10>6){
				sgg.image =sg5;
				sgg.fs =10;
			}
			
		
		
		sgArr[sgj] = sgg;
		sgj++;
	}
	for(var i =0; i<sgArr.length; i++){
		sgArr[i].y += sgY;
		if(sgArr[i].y<cteHeight){
			//判断是否接到可回收垃圾
			if(wtjpFun(sgArr[i], pann, 10)&&!sgArr[i].panS){
				sgArr[i].panX = sgArr[i].x-pann.x;
				sgArr[i].panS = true;
				sgGs++;
				fs += sgArr[i].fs;
				Fsjs();
				SgYx(); //播放音效
			}
			cte.save();
			if(sgArr[i].panS){
				if(sgGs%10==0){   //垃圾要消失
					sgArr[i].hid = true;
				}else{
					sgArr[i].y = pann.y-10;
				}
				sgArr[i].x = pann.x+sgArr[i].panX; //设置垃圾的X轴
			}else{
				sgArr[i].red += sgArr[i].redC;
			}
			cte.translate(sgArr[i].x, sgArr[i].y);
			cte.rotate(sgArr[i].red * Math.PI/180);
			if(!sgArr[i].hid){cte.drawImage(sgArr[i].image, -(sgArr[i].image.width/2), -(sgArr[i].image.height/2));}
			cte.restore();
		}else{
			if(!sgArr[i].panS){
				vita--;
				sgArr[i].panS = true;
			}
		}
	}
}
//游戏结束
function GameOver(){
	gamev = false;
	$("#play").show();
	clearInterval(erval);
}
//分数
function Fsjs(){
	if(sgGs%10 == 0){
		ps++;
		if(vita<10){
			vita+=0.1;
		}
		if(sgJg>20){
			sgJg -=5;
			sgY += 0.2;
		}
	}
}
//绘制分数和血
function DarwFsb(){
	cte.drawImage(fsb, cteWidth-152, 2);
	cte.drawImage(xue, 2, 2);
	cte.font = "12pt Arial";
	cte.fillText(""+ps, cteWidth-110, 23);
	cte.fillText(""+fs, cteWidth-40, 23);
	cte.save();
	if(vita>3){
		cte.fillStyle = "#12ff00";
	}else{
		cte.fillStyle = "red";
	}
	cte.fillRect(25, 8, vita*12, 18);
	cte.restore();
	if(vita<=0){
		GameOver();
		$(".ps").html("盘数："+ps);
		$(".fs").html("最高分："+fs);
		final_mark = fs;
	}
}
//游戏暂停或开始
function Sos(){
	gamev = !gamev;
	if(gamev){
		$("#play").hide();
		erval = setInterval(HzFun, 5);
	}else{
		clearInterval(erval);
	}
}
//不可回收垃圾
function SzZd(){
	if(pic%2==0){  //不可回收垃圾图片x轴
		zdiX = 0;
	}else{
		zdiX = 26;
	}
	if(pic%zdJg==0 && pic!=0){
		var zdd = new Zd();
		zdd.x = Math.random()*cteWidth;
		zdd.y = -20;
		zdd.image = zd;
		zdd.redC = -(Math.random()*6-3);
		zdArr[zdj] = zdd;
		zdj++;
		zdY +=0.25; //不可回收垃圾下落速度
	}
	for(var i = 0; i<zdArr.length; i++){
		if(wtjpFun(zdArr[i], pann, 10)&&!zdArr[i].hid){
			zdArr[i].hid = true;
			vita--;
			ZdYx(); //播放炸弹音效
		}
		if(zdArr[i].y>cteHeight){
			zdArr[i].hid = true;
		}		
	}
}
//绘制不可回收垃圾
function DrawZd(){
	SzZd();
	for(var i = 0; i<zdArr.length; i++){
		zdArr[i].y += zdY;
		if(!zdArr[i].hid){
			cte.save();
			zdArr[i].red += zdArr[i].redC;
			cte.translate(zdArr[i].x, zdArr[i].y);
			cte.rotate(zdArr[i].red * Math.PI/180);
			cte.drawImage(zdArr[i].image, zdiX, 0, 26, 40, -(13), -(26), 26, 40);
			cte.restore();
		}
	}
}
//碰撞
function wtjpFun(obj1, obj2, fewi){
	var A1 = obj1.x + fewi;
	var B1 = obj1.x + obj1.image.width - fewi;
	var C1 = obj1.y + fewi;
	var D1 = obj1.y + obj1.image.height - fewi;
	var A2 = obj2.x + fewi;
	var B2 = obj2.x + obj2.image.width - fewi;
	var C2 = obj2.y + fewi;
	var D2 = obj2.y + obj2.image.height - fewi;
	//判断X轴是否重叠
	if(((A1 > A2)&&(A1 < B2)) || ((B1 > A2)&&(B1 < B2))){
		//判断Y轴是否重叠
		if(((C1 > C2)&&(C1 < D2)) || ((D1 > C2)&&(D1 < D2))){
			return true;
		}	
	}
	return false;
}

function HzFun(){
	cte.clearRect(0,0,cteWidth,cteHeight);
	cte.drawImage(bg, 0, 0, bgW, bgH);
	cte.drawImage(menn.image, menn.x, menn.y);
	//绘制垃圾桶
	pann.x = menn.x+10;
	pann.y = menn.y+50;
	cte.drawImage(pann.image, pann.x, pann.y);
	HzSg(); //绘制可回收垃圾
	DrawZd(); // 绘制不可回收垃圾
	DarwFsb();//绘制分数
	zdj = 0; //不可回收垃圾间隔
	zdJg = 800; //不可回收垃圾间隔
	zdY = speed;
	pic++;
}
function getmark(){
	return final_mark;
	
}
function LoadImg(){
	men.src = "images/men.png";
	sg1.src = "images/laji.png";
	sg2.src = "images/laji2.png";
	sg3.src="images/sg3.png";
	pan.src = "images/pan.png";
	fsb.src = "images/fsb.png";
	xue.src = "images/xue.png";
	bg.src = "images/bg.png";
	zd.src = "images/sg4.png";
	sg5.src = "images/sg5.png";
	menn.image = men;
	pann.image = pan;
	bg.onload = function(){HzFun();}  //初始化后面
}

function Add(){
	document.getElementById("canvas").addEventListener('touchstart', youfun);
	document.getElementById("canvas").addEventListener('touchmove', youfun);
	document.getElementById("canvas").addEventListener('touchend', youfun);
	document.getElementById("canvas").onmousemove = function(e){ //pc获取鼠标坐标
		menn.x = e.x - menn.image.width/2;
	}
	function youfun(e){
		e.preventDefault();
		menn.x = e.targetTouches[0].pageX-100;   //安卓手机 要用 targetTouches[0].pageX 
	}
	$("#play").click(function(){
		sgJg = 100; //垃圾间隔
		ps = 0; //盘数
		fs = 0; //分数
		vita = 10; //生命
		pic = 0; //帧数
		sgj = 0; //垃圾j
		sgGs = 0; //接到可回收垃圾个数
		sgY = speed; //降落速度
		sgArr = [];
		Sos();
	});
}
$(document).ready(function(){
	bgW = $(window).width();
	bgH = $(window).height();
	$("#content").css({width: bgW, height: bgH});
	$("#content").append("<canvas id=\"canvas\" width=\""+bgW+"\" height=\""+bgH+"\">您的浏览器不支持HTML5</canvas>"); //设置画布长高
	$("#play").css({left: (bgW/2)-($("#play").width()/2), top: (bgH/2)-($("#play").height()/2)});
	
	Add();
	LoadImg();
	cte = document.getElementById("canvas").getContext("2d");
	cteWidth = parseInt($("#canvas").width());
	cteHeight = parseInt($("#canvas").height());
	menn.x = parseInt(cteWidth/2-100);
	menn.y = cteHeight-150;
	
	//document.getElementById("bgmu").volume = 0.6;  //设置背景音乐音量
});
