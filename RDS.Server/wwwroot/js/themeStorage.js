window.themeStorage = {
    // Salva a preferência de tema no localStorage
    saveThemePreference: function (isDarkMode) {
        try {
            localStorage.setItem('skillswap-theme', isDarkMode ? 'dark' : 'light');
            console.log('Tema salvo:', isDarkMode ? 'dark' : 'light');
        } catch (error) {
            console.error('Erro ao salvar tema:', error);
        }
    },

    // Recupera a preferência de tema do localStorage
    getThemePreference: function () {
        try {
            const savedTheme = localStorage.getItem('skillswap-theme');
            if (savedTheme === null) {
                // Se não há preferência salva, usar preferência do sistema
                return window.matchMedia('(prefers-color-scheme: dark)').matches;
            }
            return savedTheme === 'dark';
        } catch (error) {
            console.error('Erro ao recuperar tema:', error);
            // Fallback para tema escuro em caso de erro
            return true;
        }
    },

    // Remove a preferência salva (opcional)
    clearThemePreference: function () {
        try {
            localStorage.removeItem('skillswap-theme');
            console.log('Preferência de tema removida');
        } catch (error) {
            console.error('Erro ao remover preferência de tema:', error);
        }
    }
};