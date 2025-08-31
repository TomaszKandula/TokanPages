import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleListingAction } from "../../../Store/Actions";
import { useDimensions } from "../../../Shared/Hooks";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { ArticleListProps, SearchInputProps } from "./Types";
import { ArticleListView } from "./View/articleListView";

export const ArticleList = (props: ArticleListProps): React.ReactElement => {
    const media = useDimensions();
    const dispatch = useDispatch();
    const article = useSelector((state: ApplicationState) => state.articleListing);
    const content = useSelector((state: ApplicationState) => state.contentPageData.components.pageArticles);

    const [searchInput, setSearchInput] = React.useState<SearchInputProps>({ searchInput: "" });

    const onKeyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                onClickSearch();
            }
        },
        [searchInput]
    );

    const onInputHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setSearchInput({ ...searchInput, [event.currentTarget.name]: event.currentTarget.value });
        },
        [searchInput]
    );

    const onClickSearch = React.useCallback(() => {
        console.log("search");
    }, []);

    const onClickClear = React.useCallback(() => {
        console.log("clear");       
    }, []);

    React.useEffect(() => {
        if (article.isLoading) {
            return;
        }

        if (article.payload.results.length === 0) {
            dispatch(ArticleListingAction.get(1, 4));
        }
    }, [article.isLoading, article.payload.results]);

    return (
        <ArticleListView
            isLoading={article.isLoading}
            isMobile={media.isMobile}
            articles={article.payload.results}
            className={props.className}
            title={content?.caption}
            placeholder={content?.labels?.placeholder}
            text={content?.content}
            onKeyUp={onKeyHandler}
            onChange={onInputHandler}
            value={searchInput}
            buttonSearch={{
                label: content?.labels?.buttonSearch,
                onClick: onClickSearch,
            }}
            buttonClear={{
                label: content?.labels?.buttonClear,
                onClick: onClickClear,
            }}
        />
    );
};
