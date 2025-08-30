import * as React from "react";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";
import { ArticleCard, Icon, ProgressBar, Skeleton, TextField } from "../../../../Shared/Components";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ArticleListProps } from "../articleList";
import { v4 as uuidv4 } from "uuid";
import "./articleListView.css";

interface ArticleListViewProps extends ViewProperties, ArticleListProps {
    isMobile: boolean;
    articles: ArticleItem[];
    title: string;
    placeholder: string;
    buttonSearch: string;
    buttonClear: string;
    content: string[];
}

interface RenderContentProps {
    articles: ArticleItem[];
}

interface RenderStaticTextProps {
    isLoading: boolean;
    title: string;
    text: string[];
    placeholder: string;
    buttonSearch: string;
    buttonClear: string;
}

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

// TODO: add logic to handle interactions
const RenderStaticText = (props: RenderStaticTextProps): React.ReactElement => (
    <div className="bulma-card mb-6">
        <div className="bulma-card-content">
            <div className="bulma-content">
                <Skeleton isLoading={props.isLoading} mode="Text" height={32}>
                    <h1 className="is-size-4 has-text-weight-normal">{props.title}</h1>
                </Skeleton>
                <hr />
                {props.text.map((value: string, index: number) => (
                    <Skeleton isLoading={props.isLoading} mode="Text" key={uuidv4()}>
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
                            uuid="search-input"
                            value=""
                            placeholder={props.placeholder}
                            isLoading={false}
                            isDisabled={props.isLoading}
                            className="is-flex is-flex-grow-1"
                            startIcon={<Icon name="Magnify" size={1.5} className="has-text-link" />}
                        />
                    </div>
                    <div className="bulma-column article-list-view-search-buttons">
                        <button onClick={() => {}} className="bulma-button bulma-is-link bulma-is-light">
                            {props.buttonSearch}
                        </button>
                        <button onClick={() => {}} className="bulma-button bulma-is-danger bulma-is-light">
                            {props.buttonClear}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
);

export const ArticleListView = (props: ArticleListViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet pb-6">
            <div className={props.isMobile ? "p-4" : "py-4"}>
                <RenderStaticText
                    isLoading={props.isLoading}
                    title={props.title}
                    text={props.content}
                    placeholder={props.placeholder}
                    buttonSearch={props.buttonSearch}
                    buttonClear={props.buttonClear}
                />
                {props.isLoading ? <ProgressBar /> : <RenderContent articles={props.articles} />}
            </div>
        </div>
    </section>
);
