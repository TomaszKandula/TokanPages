import React from "react";
import { useQuery } from "../UseQuery";
import { SetPageParam, UpdatePageParam } from "../../Services/Utilities";
import Validate from "validate.js";

interface UsePageProps {
    title: string | undefined;
    page: number;
}

// When no 'title', it should always fallback to empty string.
// Wnen no 'page', it should always fallback to first page.
// URL query param is only set or updated, when no 'title' is given.
export const useArticleQuery = (): UsePageProps => {
    const queryParam = useQuery();

    const [title, setTitle] = React.useState<string | undefined>(undefined);
    const [page, setPage] = React.useState(0);

    const queryTitle = queryParam.get("title");
    const queryPage = queryParam.get("page");

    React.useEffect(() => {
        if (title === undefined) {
            const value = queryTitle === null ? "" : queryTitle;
            setTitle(value);
        }

        if (page === 0) {
            if (queryPage === null) {
                setPage(1);
                if (queryTitle === null) {
                    SetPageParam(1);
                }

                return;
            }

            if (Validate.isEmpty(queryPage)) {
                setPage(1);
                if (queryTitle === null) {
                    UpdatePageParam(1);
                }
            } else {
                try {
                    const value = parseInt(queryPage);
                    setPage(value);
                } catch {
                    setPage(1);
                    if (queryTitle === null) {
                        UpdatePageParam(1);
                    }
                }
            }
        }
    }, [title, page, queryTitle, queryPage]);

    return {
        title: title,
        page: page,
    };
};
