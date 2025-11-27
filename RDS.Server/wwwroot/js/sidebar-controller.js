// Sidebar Controller JavaScript
window.sidebarState = {
    isOpen: true,
    isMobile: false,
    dotNetRef: null,
}

window.initializeSidebar = (dotNetRef) => {
    window.sidebarState.dotNetRef = dotNetRef

    // Verifica se é mobile na inicialização
    checkMobileAndUpdate()

    // Listener para mudanças de tamanho da tela
    window.addEventListener("resize", debounce(handleResize, 250))

    // Listener para ESC key
    document.addEventListener("keydown", (e) => {
        if (e.key === "Escape" && window.sidebarState.isOpen && window.sidebarState.isMobile) {
            setSidebarState(false)
        }
    })

    // Auto-hide em mobile na inicialização
    if (window.sidebarState.isMobile) {
        setSidebarState(false)
    }
}

window.checkIfMobile = () => {
    return window.innerWidth < 992
}

function checkMobileAndUpdate() {
    const wasMobile = window.sidebarState.isMobile
    window.sidebarState.isMobile = window.innerWidth < 992

    return wasMobile !== window.sidebarState.isMobile
}

function handleResize() {
    const mobileChanged = checkMobileAndUpdate()

    if (mobileChanged) {
        // Se mudou de desktop para mobile
        if (window.sidebarState.isMobile) {
            setSidebarState(false)
        }
        // Se mudou de mobile para desktop
        else {
            setSidebarState(true)
        }
    }
}

window.setSidebarState = (isOpen) => {
    window.sidebarState.isOpen = isOpen

    // Notifica o Blazor sobre a mudança
    if (window.sidebarState.dotNetRef) {
        window.sidebarState.dotNetRef.invokeMethodAsync("UpdateSidebarState", isOpen, window.sidebarState.isMobile)
    }
}

window.toggleSidebar = () => {
    setSidebarState(!window.sidebarState.isOpen)
}

// Utility function para debounce
function debounce(func, wait) {
    let timeout
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout)
            func(...args)
        }
        clearTimeout(timeout)
        timeout = setTimeout(later, wait)
    }
}

// Cleanup quando a página é descarregada
window.addEventListener("beforeunload", () => {
    if (window.sidebarState.dotNetRef) {
        window.sidebarState.dotNetRef.dispose()
    }
})
