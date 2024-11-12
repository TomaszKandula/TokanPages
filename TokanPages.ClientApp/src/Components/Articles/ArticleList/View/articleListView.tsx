import * as React from "react";
import Container from "@material-ui/core/Container";
import { Box, Grid } from "@material-ui/core";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";
import { ProgressBar } from "../../../../Shared/Components";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ArticleCard } from "../../../Articles";
import { ArticleListProps } from "../articleList";

interface ArticleListViewProps extends ViewProperties, ArticleListProps {
    articles: ArticleItem[];
}

const RenderContent = (args: { articles: ArticleItem[] }): React.ReactElement => {
    return (
        <>
            {args.articles.map((item: ArticleItem) => (
                <ArticleCard
                    title={item.title}
                    description={item.description}
                    id={item.id}
                    key={item.id}
                    languageIso={item.languageIso}
                />
            ))}
        </>
    );
};

export const ArticleListView = (props: ArticleListViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container">
                <Box pt={0} pb={8}>
                    <Grid container justifyContent="center">
                        <Grid item xs={12} sm={12}>
                            {props.isLoading ? <ProgressBar /> : <RenderContent articles={props.articles} />}
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
};
