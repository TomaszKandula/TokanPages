export const SetPageParam = (page: number): void => {
    const base = `${window.location.origin}${window.location.pathname}`;
    window.history.pushState({}, "", `${base}?page=${page}`);
};

export const UpdatePageParam = (page: number): void => {
    const base = `${window.location.origin}${window.location.pathname}`;
    window.history.replaceState({}, "", `${base}?page=${page}`);
};
