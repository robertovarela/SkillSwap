// Fullscreen Logic
function setupFullscreen() {
    const fullscreenBtn = document.querySelector('[data-toggle="fullscreen"]');

    if (fullscreenBtn) {
        fullscreenBtn.addEventListener("click", function (e) {
            e.preventDefault();
            document.body.classList.toggle("fullscreen-enable");

            if (!document.fullscreenElement &&
                !document.mozFullScreenElement &&
                !document.webkitFullscreenElement) {
                // Enter fullscreen
                if (document.documentElement.requestFullscreen) {
                    document.documentElement.requestFullscreen();
                } else if (document.documentElement.mozRequestFullScreen) {
                    document.documentElement.mozRequestFullScreen();
                } else if (document.documentElement.webkitRequestFullscreen) {
                    document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
                }
            } else {
                // Exit fullscreen
                if (document.cancelFullScreen) {
                    document.cancelFullScreen();
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } else if (document.webkitCancelFullScreen) {
                    document.webkitCancelFullScreen();
                }
            }
        });
    }

    // Monitor fullscreen change events
    document.addEventListener("fullscreenchange", handleFullscreenChange);
    document.addEventListener("webkitfullscreenchange", handleFullscreenChange);
    document.addEventListener("mozfullscreenchange", handleFullscreenChange);
}

function handleFullscreenChange() {
    if (!document.webkitIsFullScreen &&
        !document.mozFullScreen &&
        !document.msFullscreenElement) {
        document.body.classList.remove("fullscreen-enable");
    }
}

setupFullscreen();

