import { useHead } from "unhead";

export const useUnhead = (page?: string): void => {
    const title = page ?? "software developer";

    useHead(window.__UNHEAD__ ,{
        title: "tomkandula",
        titleTemplate: `%s | ${title}`,
    });
}
