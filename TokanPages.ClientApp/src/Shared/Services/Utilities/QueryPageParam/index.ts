export const SetPageParam = (page: number): void => {
    window.history.pushState({}, "", `${window.location.href}?page=${page}`);
}

export const UpdatePageParam = (page: number): void => {
    const base = `${window.location.origin}${window.location.pathname}`;
    window.history.replaceState({}, "", `${base}?page=${page}`);
}
