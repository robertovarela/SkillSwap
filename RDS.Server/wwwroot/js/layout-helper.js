window.layoutHelper = {
    dotNetRef: null,

    initialize: function(dotNetObjectReference) {
        console.log('Layout helper initialized'); // Debug
        this.dotNetRef = dotNetObjectReference;

        // Verificação inicial
        this.checkViewport();

        // Listener para mudanças de viewport
        window.addEventListener('resize', this.debounce(() => {
            this.checkViewport();
        }, 250));

        // Listener para orientação em mobile
        window.addEventListener('orientationchange', () => {
            setTimeout(() => {
                this.checkViewport();
            }, 100);
        });
    },

    checkViewport: function() {
        if (this.dotNetRef) {
            const isMobile = window.innerWidth <= 768;
            console.log('Viewport check:', window.innerWidth, 'isMobile:', isMobile); // Debug
            this.dotNetRef.invokeMethodAsync('OnWindowResize', isMobile);
        }
    },

    debounce: function(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }
};

// Debug inicial
console.log('Layout helper script loaded');

// Função para fechar sidebar em telas pequenas (compatibilidade)
function closeSidebarOnSmallScreen() {
    if (window.innerWidth <= 768 && window.layoutHelper.dotNetRef) {
        window.layoutHelper.dotNetRef.invokeMethodAsync('OnWindowResize', true);
    }
}