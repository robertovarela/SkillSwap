window.timezoneHelper = {
    getUserTimeZone: () => {
        return Intl.DateTimeFormat().resolvedOptions().timeZone;
    }
};
