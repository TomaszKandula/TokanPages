import * as React from "react";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, Grid, IconButton, Typography } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import Validate from "validate.js";
import useStyles from "./Hooks/styleArticleDetail";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/selectArticleActions";
import CenteredCircularLoader from "../../Shared/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../Shared/ContentRender/renderContent";
import { FormatDateTime } from "../../Shared/helpers";
import { UpdateArticle } from "../../Api/Services/articles";

export interface IArticleDetail
{
    id: string;
}

export default function ArticleDetail(props: IArticleDetail) 
{
    const classes = useStyles();
    const selection = useSelector((state: IApplicationState) => state.selectArticle);
    const dispatch = useDispatch();

    if (Validate.isEmpty(selection.article.id) && !selection.isLoading)
    {
        dispatch(ActionCreators.selectArticle(props.id));
    }
    
    const updateReadCount = async () => 
    {
        await UpdateArticle(
        {
            id: props.id,
            addToLikes: 0,
            upReadCount: true
        });
    };

    const renderContent = () =>
    {
        if (Validate.isEmpty(selection.article.id) || selection.isLoading)
        {
            return(<CenteredCircularLoader />);
        }
        else
        {
            updateReadCount();
            return(<RenderContent items={selection.article.text} />);
        }
    };

    return (
        <section>
            <Container className={classes.container}>
                <Box py={12}>
                    <Grid container spacing={3}>
                        <Grid item xs={6}>
                            <Link to="/articles">
                                <IconButton>
                                    <ArrowBack/>
                                </IconButton>
                            </Link> 
                        </Grid>
                        <Grid item xs={6}>
                            <Typography className={classes.readCount} component="p" variant="subtitle1" align="right">
                                Read: {selection.article.readCount}
                            </Typography>
                        </Grid>
                    </Grid>
                    <Divider className={classes.divider} />
                    <Box mb={5}>
                        <Typography component="p" variant="subtitle1">
                            Published at: {FormatDateTime(selection.article.createdAt)}
                        </Typography>
                        <Typography component="p" variant="subtitle2" color="textSecondary">
                            Updated at: {FormatDateTime(selection.article.updatedAt)}
                        </Typography>
                    </Box>
                    <div data-aos="fade-up">
                        {renderContent()}
                    </div>
                </Box>
            </Container>
        </section>
    );
}
