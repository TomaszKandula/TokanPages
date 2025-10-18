import * as React from "react";
import { ArticleCategory } from "../../../../Api/Models";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { ArticleCard, Icon, IconButton, ProgressBar, Skeleton, TextField } from "../../../../Shared/Components";
import {
    ArticleListViewProps,
    RenderContentProps,
    RenderFilteringProps,
    RenderHeaderProps,
    RenderSortProps,
} from "../Types";
import { v4 as uuidv4 } from "uuid";

const RenderContent = (props: RenderContentProps): React.ReactElement => {
    if (props.articles.length === 0 && !props.isSnapshot) {
        return (
            <div className="is-flex is-flex-direction-column is-align-items-center is-align-content-center is-gap-1.5">
                <Icon name="FolderSearch" size={8} className="has-text-grey-light" />
                <p className="is-size-3 has-text-weight-medium has-text-grey-dark">{props.searchEmptyText1}</p>
                <p className="is-size-6 has-text-weight-light has-text-grey-dark">{props.searchEmptyText2}</p>
            </div>
        );
    } else {
        <></>;
    }

    return (
        <>
            {props.articles.map((item: ArticleItem) => (
                <ArticleCard
                    title={item.title}
                    description={item.description}
                    id={item.id}
                    key={item.id}
                    languageIso={item.languageIso}
                    canAnimate={false}
                    canDisplayDate={true}
                    published={GetDateTime({ value: item.createdAt, hasTimeVisible: false })}
                    readCount={item.readCount}
                    totalLikes={item.totalLikes}
                />
            ))}
        </>
    );
};

const RenderHeader = (props: RenderHeaderProps): React.ReactElement => (
    <div className="bulma-content">
        <Skeleton isLoading={props.isContentLoading} mode="Text" height={40}>
            <h2 className="bulma-title has-text-grey-dark">{props.title}</h2>
        </Skeleton>
        {props.text.map((value: string, index: number) => (
            <Skeleton isLoading={props.isContentLoading} mode="Text" height={20} key={uuidv4()}>
                {index === 0 ? (
                    <h3 className="bulma-subtitle has-text-grey-dark line-height-22">{value}</h3>
                ) : (
                    <p className="is-size-5 has-text-grey-dark line-height-22">{value}</p>
                )}
            </Skeleton>
        ))}
    </div>
);

const RenderFiltering = (props: RenderFilteringProps): React.ReactElement => (
    <div className="is-flex is-align-items-center is-gap-1.5 my-6">
        <TextField
            uuid="value"
            value={props.searchInputForm.value}
            onKeyUp={props.onKeyUp}
            onChange={props.onChange}
            placeholder={props.placeholder}
            isLoading={false}
            isDisabled={props.isContentLoading}
            className="is-flex is-flex-grow-1"
            startIcon={<Icon name="Magnify" size={1.5} className="has-text-link" />}
        />
        <div className="is-flex is-gap-1.5">
            <Skeleton isLoading={props.isContentLoading} mode="Circle" disableMarginY>
                <IconButton
                    size={2.2}
                    onClick={props.buttonSearch.onClick}
                    isDisabled={props.buttonSearch.isSearchDisabled}
                    hasGreyBackground
                >
                    <Icon name="FilterOutline" size={1.5} />
                </IconButton>
            </Skeleton>
            <Skeleton isLoading={props.isContentLoading} mode="Circle" disableMarginY>
                <IconButton
                    size={2.2}
                    onClick={props.buttonClear.onClick}
                    isDisabled={props.buttonClear.isClearDisabled}
                    hasGreyBackground
                >
                    <Icon name="FilterRemoveOutline" size={1.5} />
                </IconButton>
            </Skeleton>
            <Skeleton isLoading={props.isContentLoading} mode="Circle" disableMarginY>
                {props.isOrderByAscending ? (
                    <RenderSortAZ onSortClick={props.onSortClick} isDisabled={props.isDisabled} />
                ) : (
                    <RenderSortZA onSortClick={props.onSortClick} isDisabled={props.isDisabled} />
                )}
            </Skeleton>
        </div>
    </div>
);

const RenderSortAZ = (props: RenderSortProps): React.ReactElement => (
    <IconButton size={2.2} onClick={props.onSortClick} isDisabled={props.isDisabled} hasGreyBackground>
        <Icon name="SortAlphabeticalAscending" size={1.5} />
    </IconButton>
);

const RenderSortZA = (props: RenderSortProps): React.ReactElement => (
    <IconButton size={2.2} onClick={props.onSortClick} isDisabled={props.isDisabled} hasGreyBackground>
        <Icon name="SortAlphabeticalDescending" size={1.5} />
    </IconButton>
);

const RenderPagination = (props: ArticleListViewProps): React.ReactElement => {
    if (props.pageData.totalSize <= props.pageData.pageSize) {
        return <div className="mb-4">&nbsp;</div>;
    }

    const paginationItem = [];
    for (let index = 1; index <= props.pageData.paginationNumber; index++) {
        if (props.pageData.pageNumber === index) {
            paginationItem.push(
                <li key={uuidv4()}>
                    <a
                        onClick={() => props.pageData.onClick(index)}
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
                        onClick={() => props.pageData.onClick(index)}
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
            className="bulma-pagination bulma-is-rounded bulma-is-small bulma-is-centered my-6"
            role="navigation"
            aria-label="pagination"
        >
            <ul className="bulma-pagination-list">{paginationItem}</ul>
        </nav>
    );
};

const RenderCategories = (props: ArticleListViewProps): React.ReactElement => (
    <div className="bulma-tags m-0 pt-4">
        <Skeleton isLoading={props.isContentLoading} height={40}>
            {props.categories.map((value: ArticleCategory, _index: number) => (
                <span
                    key={value.id}
                    className={`bulma-tag bulma-is-medium is-clickable ${props.selectedCategory === value.id ? "bulma-is-dark" : ""}`}
                    onClick={() => props.onCategoryChange(value.id)}
                >
                    {value.categoryName}
                </span>
            ))}
        </Skeleton>
    </div>
);

export const ArticleListView = (props: ArticleListViewProps): React.ReactElement => {
    const isFilterDisabled = props.articles.length === 0 || props.articles.length === 1;

    return (
        <section className={props.className}>
            <div className="bulma-container bulma-is-max-tablet pb-6">
                <div className={props.isMobile ? "p-4" : "py-4"}>
                    <RenderHeader {...props} />
                    <RenderCategories {...props} />
                    <RenderFiltering {...props} isDisabled={isFilterDisabled} />
                    {props.isLoading ? (
                        <ProgressBar />
                    ) : (
                        <>
                            <RenderContent {...props} />
                            <RenderPagination {...props} />
                        </>
                    )}
                </div>
            </div>
        </section>
    );
};
