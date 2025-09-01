import * as React from "react";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";
import { ArticleCard, Icon, ProgressBar, Skeleton, TextField } from "../../../../Shared/Components";
import { ArticleListViewProps, RenderContentProps, RenderStaticTextProps } from "../Types";
import { v4 as uuidv4 } from "uuid";
import "./articleListView.css";

const RenderContent = (props: RenderContentProps): React.ReactElement => (
    <>
        {props.articles.map((item: ArticleItem) => (
            <ArticleCard
                title={item.title}
                description={item.description}
                id={item.id}
                key={item.id}
                languageIso={item.languageIso}
                canAnimate={true}
                readCount={item.readCount}
                totalLikes={item.totalLikes}
            />
        ))}
    </>
);

const RenderStaticText = (props: RenderStaticTextProps): React.ReactElement => (
    <div className="bulma-card">
        <div className="bulma-card-content">
            <div className="bulma-content">
                <Skeleton isLoading={props.isContentLoading} mode="Text" height={32}>
                    <h1 className="is-size-4 has-text-weight-normal">{props.title}</h1>
                </Skeleton>
                <hr />
                {props.text.map((value: string, index: number) => (
                    <Skeleton isLoading={props.isContentLoading} mode="Text" key={uuidv4()}>
                        {index === 0 ? (
                            <h2 className="is-size-6 has-text-grey has-text-weight-medium line-height-18">{value}</h2>
                        ) : (
                            <p className="is-size-6 has-text-grey line-height-18">{value}</p>
                        )}
                    </Skeleton>
                ))}
                <hr />
                <div className="bulma-columns">
                    <div className="bulma-column article-list-view-search-bar">
                        <TextField
                            uuid="searchInput"
                            value={props.value.searchInput}
                            onKeyUp={props.onKeyUp}
                            onChange={props.onChange}
                            placeholder={props.placeholder}
                            isLoading={false}
                            isDisabled={props.isContentLoading}
                            className="is-flex is-flex-grow-1"
                            startIcon={<Icon name="Magnify" size={1.5} className="has-text-link" />}
                        />
                    </div>
                    <div className="bulma-column article-list-view-search-buttons">
                        <Skeleton isLoading={props.isContentLoading} mode="Rect" height={40} disableMarginY>
                            <button
                                onClick={props.buttonSearch.onClick}
                                className="bulma-button bulma-is-link bulma-is-light"
                                disabled={props.buttonSearch.isSearchDisabled}
                            >
                                {props.buttonSearch.label}
                            </button>
                        </Skeleton>
                        <Skeleton
                            isLoading={props.isContentLoading}
                            mode="Rect"
                            height={40}
                            disableMarginY
                            className="ml-3"
                        >
                            <button
                                onClick={props.buttonClear.onClick}
                                className="bulma-button bulma-is-danger bulma-is-light"
                                disabled={props.buttonClear.isClearDisabled}
                            >
                                {props.buttonClear.label}
                            </button>
                        </Skeleton>
                    </div>
                </div>
            </div>
        </div>
    </div>
);

const RenderPagination = (props: ArticleListViewProps): React.ReactElement => {
    if (props.pageData.totalSize <= props.pageData.pageSize) {
        return <div className="mb-4">&nbsp;</div>;
    }

    const pageNumber = props.pageData.pageNumber;
    const pages = Math.round(props.pageData.totalSize / props.pageData.pageSize);
    const paginationItem = [];

    for (let index = 1; index <= pages; index++) {
        if (pageNumber === index) {
            paginationItem.push(
                <li key={uuidv4()}>
                    <a
                        onClick={props.pageData.onClick}
                        className="bulma-pagination-link bulma-is-current"
                        aria-label={`Page-${index}`}
                        aria-current="page"
                    >
                        {index}
                    </a>
                </li>
            );
        } else {
            paginationItem.push(
                <li key={uuidv4()}>
                    <a
                        onClick={props.pageData.onClick}
                        className="bulma-pagination-link"
                        aria-label={`Page-${index}`}
                        aria-current="page"
                    >
                        {index}
                    </a>
                </li>
            );
        }
    }

    return (
        <nav
            className="bulma-pagination bulma-is-small bulma-is-centered my-6"
            role="navigation"
            aria-label="pagination"
        >
            <ul className="bulma-pagination-list">{paginationItem}</ul>
        </nav>
    );
};

export const ArticleListView = (props: ArticleListViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet pb-6">
            <div className={props.isMobile ? "p-4" : "py-4"}>
                <RenderStaticText {...props} />
                <RenderPagination {...props} />
                {props.isLoading ? <ProgressBar /> : <RenderContent articles={props.articles} />}
                <RenderPagination {...props} />
            </div>
        </div>
    </section>
);
