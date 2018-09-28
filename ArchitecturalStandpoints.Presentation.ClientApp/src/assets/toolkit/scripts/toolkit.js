;(function(window) {

'use strict';

//*///////////////////////////////////////*
//
//  Utilities
//
//*///////////////////////////////////////*

//*---------------------------------------*
//  Utility: Get the closest matching element up the DOM tree
//*---------------------------------------*
const getClosest = function closest ( elem, selector ) {	

    // Element.matches() polyfill
    if (!Element.prototype.matches) {
        Element.prototype.matches =
            Element.prototype.matchesSelector ||
            Element.prototype.mozMatchesSelector ||
            Element.prototype.msMatchesSelector ||
            Element.prototype.oMatchesSelector ||
            Element.prototype.webkitMatchesSelector ||
            function(s) {
                var matches = (this.document || this.ownerDocument).querySelectorAll(s),
                    i = matches.length;
                while (--i >= 0 && matches.item(i) !== this) {}
                return i > -1;
            };
    }

    // Get closest match
    for ( ; elem && elem !== document; elem = elem.parentNode ) {
        if ( elem.matches( selector ) ) return elem;
    }

    return null;
};

//*---------------------------------------*
//  Character Counter
//*---------------------------------------*
const counters = document.querySelectorAll('.js-character-counter');

counters.forEach(character => {
	let textareaField = character.getElementsByTagName('textarea')[0],
		maxNumber = textareaField.maxLength,
		counterField = character.querySelector('[data-character-counter]');

	// display counter
	counterField.innerHTML = maxNumber + " characters left";

	textareaField.addEventListener('keyup', function(e) {
		let textLength = textareaField.value.length;
		let textRemaining = maxNumber - textLength;
		// update counter
		counterField.innerHTML = textRemaining + " characters left";
	});
});

//*///////////////////////////////////////*
//
//  Toggle Checkboxes
//
//*///////////////////////////////////////*

//*---------------------------------------*
//  Toggle all checkboxes in a data-group
//*---------------------------------------*
const checkboxToggle = document.querySelectorAll('.js-toggle-checkbox');

checkboxToggle.forEach(checkbox => {
	let group = checkbox.getAttribute('data-checkbox-toggle');
	let checkboxes = document.querySelectorAll('[data-checkbox-toggle="' + group + '"');

	checkbox.addEventListener('click', function(e) {
		// checkbox.checked ? text.innerHTML = "Deselect All" : text.innerHTML = "Select All";
		checkboxes.forEach(checkbox => {
			let closestElem = getClosest(checkbox.parentNode, '.card--checkbox');
			if (this.checked) {
	    		checkbox.checked = true;
	    		if (closestElem) closestElem.classList.add('active')
			} else {
				checkbox.checked = false;
				if (closestElem) closestElem.classList.remove('active');
			}
		});
	});
});

//*---------------------------------------*
//  Toggle active class on .card with checkbox
//*---------------------------------------*
const checkboxInCard = document.querySelectorAll('.card--checkbox [data-checkbox-toggle]');

checkboxInCard.forEach(checkbox => {
	let closestElem = getClosest(checkbox.parentNode, '.card--checkbox');

	checkbox.addEventListener('click', function(e) {
		checkbox.checked ? closestElem.classList.add('active') : closestElem.classList.remove('active');
	});
});

//*///////////////////////////////////////*
//
//  Page Screen
//
//*///////////////////////////////////////*

//*---------------------------------------*
//  Show Page Screen
//*---------------------------------------*
const toggleScreen = document.querySelectorAll('.js-toggle-screen');

toggleScreen.forEach(toggle => {
	let id = toggle.getAttribute('data-toggle-screen');
	let screen = document.querySelector('[data-screen="' + id + '"');

	toggle.addEventListener('click', function(e) {
		screen.classList.add('show');
	});
});

//*---------------------------------------*
//  Hide Page Screen
//*---------------------------------------*
const closeScreen = document.querySelectorAll('.js-close-screen');

closeScreen.forEach(close => {
	let closestElem = getClosest(close.parentNode, '.page-screen');

	close.addEventListener('click', function(e) {
		closestElem.classList.remove('show');
	})
});

//*///////////////////////////////////////*
//
//  jQuery
//
//*///////////////////////////////////////*
jQuery(document).ready(function($) {

	//*---------------------------------------*
	//  Datepicker
	//*---------------------------------------*
	if( $('.js-datepicker').length ){
		$('.js-datepicker').daterangepicker({
	        singleDatePicker: true,
	        showDropdowns: true,
	        autoUpdateInput: false
	    });

		$('.js-datepicker').on('apply.daterangepicker', function(ev, picker) {
			$(this).val(picker.startDate.format('MM/DD/YYYY'));
		});
	}

	//*---------------------------------------*
	//  Modal
	//*---------------------------------------* 
	function modal () {
		//modal options
		$('.js-modal').magnificPopup({
		  type: 'inline',
		  preloader: false,
		  modal: false,
		  showCloseBtn: false,
		  // Delay in milliseconds before popup is removed
		  removalDelay: 300,
		  // Class that is added to popup wrapper and background
		  mainClass: 'mfp-fade'
		});
		//dismiss modal
		$(document).on('click', '.js-modal-dismiss', function (e) {
		  e.preventDefault();
		  $.magnificPopup.close();
		});
	}
	if( $('.js-modal').length ){
		modal();
	}

	//*---------------------------------------*
	//  Read More
	//*---------------------------------------* 
	if( $('.read-more').length ){
	    var showChar = 120;
	    var ellipsestext = "...";
	    var moretext = "Read All";
	    var lesstext = "Hide";
	    
	    $('.read-more').each(function() {
	    	$(this).show();
	        var content = $(this).html();
	 
	        if(content.length > showChar) {
	 
	            var c = content.substr(0, showChar);
	            var h = content.substr(showChar, content.length - showChar);
	 
	            var html = c + '<span class="moreellipses">' + ellipsestext+ '&nbsp;</span><span class="read-more-content"><span>' + h + '</span><a href="javascript:void(0);" class="js-read-more link-secondary mt-1 d-block">' + moretext + '</a></span>';
	 
	            $(this).html(html);
	        }
	 
	    });
	 
	    $(".js-read-more").click(function(){
	        if($(this).hasClass("less")) {
	            $(this).removeClass("less");
	            $(this).html(moretext);
	        } else {
	            $(this).addClass("less");
	            $(this).html(lesstext);
	        }
	        $(this).parent().prev().toggle();
	        $(this).prev().toggle();
	        return false;
	    });
	}

	//*---------------------------------------*
	//  Search 
	//*---------------------------------------*
	//Admin defined tags
	let tagsInitData = [
		{
			type: 'standard',
		 	group: 'status',
		 	name: 'Status',
		  	options: [
			 	'Pending', 
			 	'Approved', 
			 	'Finished' 
		  ]
		},
		{
			type: 'standard',
		 	group: 'bank-name',
		 	name: 'Bank Name',
		  	options: [
			 	'Pending', 
			 	'Approved', 
			 	'Finished' 
		  ]
		},
		{
			type: 'standard',
		 	group: 'as-of-date',
		 	name: 'As of Date',
		  	options: [
			 	'Pending', 
			 	'Approved', 
			 	'Finished' 
		  ]
		},
		{
			type: 'standard',
		 	group: 'delivery-method',
		 	name: 'Delivery Method',
		  	options: [
			 	'Pending', 
			 	'Approved', 
			 	'Finished' 
		  ]
		},
		{
			type: 'standard',
		 	group: 'form-name',
		 	name: 'Form Name',
		  	options: [
			 	'Pending', 
			 	'Approved', 
			 	'Finished' 
		  ]
		},
		{
			type: 'standard',
		 	group: 'engagement',
		 	name: 'Engagement #',
		  	options: [
			 	'Pending', 
			 	'Approved', 
			 	'Finished' 
		  ]
		},
	];

	// MAIN selected tags source of truth
	let tagsList = [];

	// Tags container 
	$('.bootstrap-tagsinput').prepend('<div id="standard-tags-wrap"></div>');
   
  	// Add Admin tags to filter dropdown
	$.each(tagsInitData, function(i, tag) {
		$('#select-tagList').append(`<li data-group='${tag.group}'>${tag.name}</li>`);
	})

	let selectCount = 0;

  	// Add slected admin tag to search bar
	$('#select-tagList').on('click', function(e) {
		let target = $(e.target);

		if (target.is('li')) {

			let tag = target.text();
			let group = target.attr('data-group');
			let indexOfGroup = getIndexOfTag('standard', group);

	    if (typeof indexOfGroup !== 'undefined') {
	    	return; //Dont add if duplicate;
	    }

			let groupObj = getTagObj(group);
			let selectedObj = {
				type: groupObj.type,
				group: groupObj.group,
				selected: groupObj.options[0]
			};

			let id = "select" + selectCount++;
			$('#standard-tags-wrap').append(`
				<select id="${id}" class="selectpicker" data-style="${id}" data-width="100%">
					${groupObj.options.map(option => `<option value=${option}>${option}</option>`)}
				</select>
			`)

		  $('.selectpicker').selectpicker({
		  	showSubtext: true
		  });

		  $(`.${id} .filter-option`).attr('data-tag', groupObj.name + ':');
      $(`.${id}`).append('<span class="close-tag">x</span>')
			selectListener(id, group);
			tagsList.push(selectedObj);
		  printList();
		}
	})
	
  	// Add user typed tag to json data source
	$("#tagsInput").on('itemAdded', function(event) {
		let userTagObj = {
			'type': 'custom',
			'selected': event.item
		} 
	  tagsList.push(userTagObj);
		printList();
	})

  	// Remove user typed tag
	$("#tagsInput").on('itemRemoved', function(event) {
		let tagIndex = getIndexOfTag('custom', event.item);
		tagsList.splice(tagIndex, 1); 
		printList();
	})

	$("#bulkCkb").on('click', function(event) {
		let isChecked = $(this).prop('checked');
		if(isChecked){
			$('#bulkAction').fadeIn();
		}else{
			$('#bulkAction').fadeOut();
		}
	})
	$(".lbl-dropdown").on('click',function(){
		let isActive = $(this)[0].classList.value.indexOf('active') > 0 ? true : false;
		if(isActive){
			$("#"+this.id+'-dropdown').hide();
			$("#"+this.id+'-dropdown')[0].classList.remove('active');
		}else{
			$("#"+this.id+'-dropdown').show();
			$("#"+this.id+'-dropdown')[0].classList.add('active');
		}
	});

	$(".drop-zone").on('dragover',function(e){
		e.preventDefault();
		$(this)[0].classList.add('dragover');
	});
	$(".drop-zone").on('dragleave',function(e){
		e.preventDefault();
		$(this)[0].classList.remove('dragover');
	});
	$(".drop-zone").on('drop',function(e){
		e.preventDefault();
		$(this)[0].classList.remove('dragover');
		//console.log();
		//$("#lbl-drop-zone").innerText = e.originalEvent.dataTransfer.files.name
	});

	$('#navigation-btn').on('click',function(){
		if($(".nav-li.active")[0]){
			let activeItem = $(".nav-li.active")[0].id;
			switch(activeItem){
				case 'account-step':
					$("#account-step")[0].classList.remove('active');
					$("#account-step")[0].classList.add('item-completed');
					$("#review-step")[0].classList.add('active');
					$("#account-pill").show();
				break;
				case 'review-step':
					$("#review-step")[0].classList.remove('active');
					$("#review-step")[0].classList.add('item-completed');
					$("#checkout-step")[0].classList.add('active');
				break;
				case 'checkout-step':
					$("#checkout-step")[0].classList.remove('active');
					$("#checkout-step")[0].classList.add('item-completed');
				break;
			}
		}
	});

	$('.progress-action').on('click', function(){
		let reviewed = $('.count-reviewed')[0].innerText;
		let review = $('.count-review')[0].innerText;
		if(reviewed < 100){
			reviewed++;
			$('.progress-container .progress-bar').css('cssText','width: '+reviewed+'px !important');
			$('.count-reviewed')[0].innerText = reviewed;	
			if(review != 100){
				review++;
				$('.count-review')[0].innerText = review;
			}
		}
	});
	function alterTableItems(thID,opacity){
		for(let i = 1; i < 6 ; i++){
			if(thID != '0'+i){
				$("#tr-0"+i).css('cssText','opacity:'+opacity+';');
			}
		}
	}
	function saveChanges(trID){
		$("#"+trID+" .spn-responder")[0].innerText = $("#"+trID+" .inp-responder")[0].value;
		$("#"+trID+" .spn-attention")[0].innerText = $("#"+trID+" .inp-attention")[0].value;
		$("#"+trID+" .spn-address1")[0].innerText = $("#"+trID+" .inp-address1")[0].value;
		$("#"+trID+" .spn-address2")[0].innerText = $("#"+trID+" .inp-address2")[0].value;
		$("#"+trID+" .spn-city")[0].innerText = $("#"+trID+" .inp-city")[0].value;
		$("#"+trID+" .spn-state")[0].innerText = $("#"+trID+" .inp-state")[0].value;
		$("#"+trID+" .spn-code")[0].innerText = $("#"+trID+" .inp-code")[0].value;
	}
	function restoreInputs(trID){	
		$("#"+trID+" .inp-responder")[0].value = $("#"+trID+" .spn-responder")[0].innerText;
		$("#"+trID+" .inp-attention")[0].value = $("#"+trID+" .spn-attention")[0].innerText;
		$("#"+trID+" .inp-address1")[0].value = $("#"+trID+" .spn-address1")[0].innerText;
		$("#"+trID+" .inp-address2")[0].value = $("#"+trID+" .spn-address2")[0].innerText;
		$("#"+trID+" .inp-city")[0].value = $("#"+trID+" .spn-city")[0].innerText;
		$("#"+trID+" .inp-state")[0].value = $("#"+trID+" .spn-state")[0].innerText;
		$("#"+trID+" .inp-code")[0].value = $("#"+trID+" .spn-code")[0].innerText;
	}
	$(".editable-lnk").on('click',function(){
		if($(".editable").length == 0){
			let thID = $(this)[0].id;
			thID = thID.split('lnk-')[1];

			$("#tr-"+thID)[0].classList.add('editable');
			alterTableItems(thID,'0.5');
		}
	});

	$(".btn-save").on('click',function(){
		let trID = $(".editable")[0].id.split("tr-")[1];
		alterTableItems(trID,'1');
		$(".editable")[0].classList.remove('editable');
		saveChanges('tr-'+trID);
		restoreInputs('tr-'+trID);
	});
	$(".btn-cancel").on('click',function(){
		let trID = $(".editable")[0].id.split("tr-")[1];
		alterTableItems(trID,'1');
		$(".editable")[0].classList.remove('editable');
		restoreInputs('tr-'+trID);
	});
	$(".inp-responder").on('keyup',function(){
		let val = $(this)[0].value;
		if(val == ''){
			$(this).parent()[0].classList.add('validation-err');
		}else{
			if( /[^a-zA-Z0-9\-\/]/.test( val )){
				//err
				$(this).parent()[0].classList.add('validation-err');;
			}else{
				$(this).parent()[0].classList.remove('validation-err');
			}
		}
	})
	$(".inp-address1").on('keyup',function(){
		let val = $(this)[0].value;
		if(val == ''){
			$(this).parent()[0].classList.add('validation-err');
		}else{
			if( /[^a-zA-Z0-9\-\/]/.test( val )){
				//err
				$(this).parent()[0].classList.add('validation-err');;
			}else{
				$(this).parent()[0].classList.remove('validation-err');
			}
		}
	})
	$(".inp-city").on('keyup',function(){
		let val = $(this)[0].value;
		if(val == ''){
			$(this).parent()[0].classList.add('validation-err');
		}else{
			if( /[^a-zA-Z0-9\-\/]/.test( val )){
				//err
				$(this).parent()[0].classList.add('validation-err');;
			}else{
				$(this).parent()[0].classList.remove('validation-err');
			}
		}
	})
	$(".inp-state").on('keyup',function(){
		let val = $(this)[0].value;
		if(val == ''){
			$(this).parent()[0].classList.add('validation-err');
		}else{
			if(val.length < 2){
				$(this).parent()[0].classList.add('validation-err');
			}else{
				$(this).parent()[0].classList.remove('validation-err');
			}
		}
	})
	$(".inp-code").on('keyup',function(){
		let val = $(this)[0].value;
		if(val == ''){
			$(this).parent()[0].classList.add('validation-err');
		}else{
			if(val.length < 5){
				$(this).parent()[0].classList.add('validation-err');
			}else{
				$(this).parent()[0].classList.remove('validation-err');
			}
		}
	})

	$("#lnk-pill-group").on('click', function(){
		$('.pill-group .active').each(function(){
			$(this)[0].classList.remove('active');
		});
	});

	$("#popover-action").on('click',function(){
		let classList = $('.card-popover')[0].classList.value;
		$(this)[0].classList.add('active');
		if(classList.indexOf('active') == -1){
			$('.card-popover').css('cssText','display:flex;');
			$('.card-popover')[0].classList.add('active');
		}else{
			$('.card-popover')[0].classList.remove('active');
			$(this)[0].classList.remove('active');
			$('.card-popover').hide();
		}
	});
	//Close popover when the user click outside of him
	$(document).mouseup(function(e) 
	{
		if( $('.card-popover')[0]){
			var container = $(".card-popover");
			if (!container.is(e.target) && container.has(e.target).length === 0) 
			{
				container[0].classList.remove('active');
				$("#popover-action")[0].classList.remove('active');
				container.hide();
			}
		}
		if( $('.dropdown-menu.active')[0]){
			var container = $(".dropdown-menu.active");
			var lblActive = container[0].id.split('-dropdown')[0];
			if (!container.is(e.target) && container.has(e.target).length === 0) 
			{
				container[0].classList.remove('active');
				$("#"+lblActive)[0].classList.remove('active');
				container.hide();
			}
		}
	});

	$('.card-intitution').on('click',function(){
		let value = $('.card-intitution')[0].classList.value;
		let isActive = value.indexOf('responder') == -1 ? true : false;
		if(isActive){
			$('.card-intitution')[0].classList.add('active');
		}else{
			$('.card-intitution')[0].classList.remove('responder');
		}
	});
	$('.change-responder').on('click',function(){
		$('.card-intitution')[0].classList.remove('active');
		$('.card-intitution')[0].classList.add('responder');
	});
	
	
	// selectListener() binds unique event listner to selected tag dropdown
	function selectListener (id, group) {
	  let currentVal;
	  let newVal;
	  let oldVal;

	  //Updates dropdown values 
		$(`#${id}`).on('show.bs.select', function(event) {
	  	currentVal = $(`#${id}`).val();
		})
		$(`#${id}`).on('changed.bs.select', function(event) {
	    let tagIndex = getIndexOfTag('standard', group);
			newVal = $(`#${id}`).val() 
			oldVal = currentVal;
		  tagsList[tagIndex].selected = newVal;
		  printList();
		})

	  //removes tag	
		$(`.${id} .close-tag`).on('click', function(event) {
    	$(this).closest(".bootstrap-select").remove();
			let tagIndex = getIndexOfTag('standard', group);
			tagsList.splice(tagIndex, 1); 
		  printList();
		})

	}

  	// getIndexOfTag() returns selected tag from initial tag list
	function getTagObj (group) {
		let selectedGroup;
		$.each(tagsInitData, function(idx, tagObj) {
			if (tagObj.group === group ) {
				selectedGroup = tagsInitData[idx]; 
			}
		})
		return selectedGroup;
	}

	/**
	 * getIndexOfTag() returns index of tag
	 * Type can equal 'standard'(admin defined) or  'custom'(user defined)
	 */
	function getIndexOfTag (type, tag) {
		let index;
 		if (type === 'standard') {
 			$.each(tagsList, function (idx, tagObj) {
	 		 	if (tagObj.group === tag) {
	 		 		index = idx;
	 		 	}
 			}) 	
 			return index;
 		}
 		if (type === 'custom') {
    	$.each(tagsList, function (idx, tagObj) {
    		if(tagObj.selected === tag) {
    			index = idx;
    		}
    	})
    	return index;
 		}
	}
  
  	// Prints JSON structure to document. FOR TESTING ONLY
	function printList () {
		$("#list-prev").html(JSON.stringify(tagsList))
	}
	printList();
});

	
})(window);