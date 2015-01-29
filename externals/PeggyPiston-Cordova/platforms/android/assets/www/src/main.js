/* globals define */
define(function(require, exports, module) {
    'use strict';

    var Engine = require('famous/core/Engine');
    var ImageSurface = require('famous/surfaces/ImageSurface');
    var Modifier = require('famous/core/Modifier');
    var Surface = require('famous/core/Surface');
    var Transform = require('famous/core/Transform');

    var YOUR_API_KEY = 'AIzaSyATj2V80z9vs1AQOgxrwL95opxYsxdUvEM';
    var mainContext = Engine.createContext();


    // onGeolocationSuccess Callback
    //   This method accepts a `Position` object, which contains
    //   the current GPS coordinates
    //
    function onGeolocationSuccess(position) {
        var mapUrl =
            'https://www.google.com/maps/embed/v1/place?q=' +
            position.coords.latitude + '%2C' + position.coords.longitude +
            '&key=' + YOUR_API_KEY;
        var mapHtml =
            '<div class="map"><iframe width="100%" height="100%" frameborder="0" src="' + mapUrl + '"></iframe></div>';
        var string =
            '<div class="text">Latitude: ' + position.coords.latitude +
            '<br/>Longitude: ' + position.coords.longitude +
            '<br/>Altitude: ' + position.coords.altitude +
            '<br/>Accuracy: ' + position.coords.accuracy +
            '<br/>Altitude Accuracy: ' + position.coords.altitudeAccuracy +
            '<br/>Heading: ' + position.coords.heading +
            '<br/>Speed: ' + position.coords.speed +
            '<br/>Timestamp: ' + position.timestamp + '</div>';
        container.setContent(mapHtml + string);
    }

    // onGeolocationError Callback receives a PositionError object
    //
    function onGeolocationError(error) {
        alert('code: ' + error.code + '\n' +
            'message: ' + error.message + '\n');
    }

    function greeting() {
        // initialize the speaking engine.
        //speechEngine = new SpeechSynthesisUtterance();
        console.log('greeting triggered');
        console.log(window.speechSynthesis);

//*

        var speechEngine = new SpeechSynthesisUtterance();
        speechEngine.lang = 'en-US';
        speechEngine.text = "Hello!  My name is Peggy Piston.  How are you doing?";
        speechSynthesis.speak(speechEngine);  
//*/

        console.log("speaking: " + speechEngine.text);


    }

    var title = new Surface({
        size: [true, true],
        content: 'why does the speechSynthesis not work?!?!',
        classes: ['title']
    });

    var container = new Surface({
        size: [350, 490],
        content: 'waiting for onDeviceReady',
        classes: ['container']
    });

    var titleModifier = new Modifier({
        origin: [1.0, 1.0],
        align: [1.0, 1.0]
    });
    var containerModifier = new Modifier({
        origin: [0.5, 0],
        align: [0.5, 0],
        transform: Transform.translate(0, 15, 0)
    });


    function onDeviceReady() {
        container.setContent('waiting for onGeolocationSuccess');
        navigator.geolocation.getCurrentPosition(onGeolocationSuccess, onGeolocationError);
/*
        var u = new SpeechSynthesisUtterance();
        u.text = "You know you're going to make the app say something naughty.";
        u.lang = 'en-US';
        speechSynthesis.speak(u);
*/

        setTimeout(function() {
            greeting();
        }, 3000);
    }
    document.addEventListener('deviceready', onDeviceReady, false);

    mainContext.add(containerModifier).add(container);
    mainContext.add(titleModifier).add(title);

});
