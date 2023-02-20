import * as React from "react";
import Container from "@material-ui/core/Container";
import { Box, Divider, Grid, IconButton } from "@material-ui/core";
import { Link } from "react-router-dom";
import { ArrowBack } from "@material-ui/icons";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";
import { ProgressBar } from "../../../../Shared/Components";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ArticleCard } from "../../../Articles";
import { ArticleListStyle } from "./articleListStyle";

interface Properties extends ViewProperties
{
    articles: ArticleItem[];
}

const RenderContent = (args: { articles: ArticleItem[] }): JSX.Element =>
{
    return(
        <>
            {args.articles.map((item: ArticleItem) => ( 
                <ArticleCard 
                    title={item.title}
                    description={item.description}
                    id={item.id}
                    key={item.id}
                />
            ))}
        </>
    );
}

export const ArticleListView = (props: Properties): JSX.Element => 
{
    const classes = ArticleListStyle();
    return (
        <section className={classes.section}>
            <Container className={classes.container}>
                <Box pt={12} pb={8}>
                    <div data-aos="fade-down">
                        <Link to="/">
                            <IconButton>
                                <ArrowBack/>
                            </IconButton>
                        </Link> 
                        <Divider className={classes.divider} />
                    </div>
                    <Grid container justifyContent="center">
                        <Grid item xs={12} sm={12}>
                            {props.isLoading 
                                ? <ProgressBar /> 
                                : <RenderContent articles={props.articles} />}
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}
