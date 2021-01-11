// Global util functions

// Extend String class with C-like String.format
if (IsUndefinedOrNull(String.format)) {
    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return !IsUndefined(args[number])
                ? args[number]
                : match
                ;
        });
    };
}

// Extend String class with C#-like String.IsNullOrEmpty
if (IsUndefinedOrNull(String.isNullOrEmpty)) {
    String.isNullOrEmpty = function (value) {
        return IsUndefinedOrNull(value) || value === '';
    };
}



// Extend String class with padLeft function
if (IsUndefinedOrNull(String.prototype.padLeft)) {
    String.prototype.padLeft = function (length, char) {
        var n = this;
        var z = char || '0';
        n = n + '';
        return n.length >= length ? n : new Array(length - n.length + 1).join(z) + n;
    };
}

// Extend String class with padRight function
if (IsUndefinedOrNull(String.prototype.padRight)) {
    String.prototype.padRight = function (length, char) {
        var n = this;
        var z = char || '0';
        n = n + '';
        return n.length >= length ? n : n + new Array(length - n.length + 1).join(z);
    };
}


function IsUndefinedOrNull(value) {
    return typeof value === "undefined" || value === null;
}

function IsUndefined(value) {
    return typeof value === "undefined";
}

// Extend $ methods with hasAttr(attrName)
if (IsUndefinedOrNull($.fn.hasAttr))
    $.fn.hasAttr = function (attrName) {
        var attr = $(this).attr(attrName);
        return typeof attr !== typeof undefined && attr !== false;
    };

// Extend $ methods with exists()
if (IsUndefinedOrNull($.fn.exists))
    $.fn.exists = function () {
        return $(this).length !== 0;
    };



// encode string to Base64 Unicode
function b64EncodeUnicode(str) {
    // first we use encodeURIComponent to get percent-encoded UTF-8,
    // then we convert the percent encodings into raw bytes which
    // can be fed into btoa.
    return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g,
        function toSolidBytes(match, p1) {
            return String.fromCharCode('0x' + p1);
        }));
}

// decode Base64 Unicode to string
function b64DecodeUnicode(str) {
    // Going backwards: from bytestream, to percent-encoding, to original string.
    return decodeURIComponent(atob(str).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
}

// parse JWT token to JSON object
function parseJwt(token) {
    var tokenPayload64 = token.split('.')[1].replace(/-/g, '+').replace(/_/g, '/');
    var tokenPayload = b64DecodeUnicode(tokenPayload64);

    return JSON.parse(tokenPayload);
};


//#region Cookies helpper
function getCookie(name) {
    var v = document.cookie.match('(^|;) ?' + name + '=([^;]*)(;|$)');
    return v ? v[2] : null;
}

function setCookie(name, value, seconds) {
    var d = new Date();
    d.setTime(d.getTime() + 1000 * seconds);

    document.cookie = String.format("{0}={1};path=/;expires={2}", name, value, d.toUTCString());
}

function setCookieWithPath(name, value, seconds, path) {
    var d = new Date();
    d.setTime(d.getTime() + 1000 * seconds);

    document.cookie = String.format("{0}={1};path={2};expires={3}", name, value, path, d.toUTCString());
}

function setSessionCookie(name, value) {
    document.cookie = String.format("{0}={1};path=/", name, value);
}


function setSessionCookieWithPath(name, value, path) {
    document.cookie = String.format("{0}={1};path={2}", name, value, path);
}

function deleteCookie(name) { setCookie(name, '', -1); }



//#endregion

//parsing jwt data into a gloabal var
var UserData = {};

function UpdateUserDataFromJWT() {
    var cookieJWT = getCookie("JWT");
    UserData = IsUndefinedOrNull(cookieJWT) ? null : parseJwt(cookieJWT);
}

