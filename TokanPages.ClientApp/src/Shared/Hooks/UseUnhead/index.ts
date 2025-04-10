import { useHead } from "@unhead/react";

export const useUnhead = (page?: string): void => {
    const title = page ?? "software developer";

    useHead({
        title: "tomkandula",
        titleTemplate: `%s | ${title}`,
    });
}
