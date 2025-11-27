document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.querySelector('.btn-toggle-sidebar');
    if (toggleButton) {
        toggleButton.addEventListener('click', function () {
            document.body.classList.toggle('sidebar-hidden');
        });
    }
});

function closeSidebarOnSmallScreen() {
    if (window.innerWidth <= 768) {
        const toggle = document.querySelector('.navbar-toggler');
        if (toggle) {
            toggle.click();
        }
    }
}
