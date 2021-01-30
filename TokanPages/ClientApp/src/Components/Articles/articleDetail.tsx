import * as React from "react";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, Grid, IconButton, Typography } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import PersonIcon from "@material-ui/icons/Person";
import Validate from "validate.js";
import useStyles from "./Hooks/styleArticleDetail";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/selectArticleActions";
import CenteredCircularLoader from "../../Shared/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../Shared/ContentRender/renderContent";
import { FormatDateTime } from "../../Shared/helpers";
import { UpdateArticle } from "../../Api/Services/articles";
import { AVATARS_PATH } from "../../Shared/constants";

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
 
        updateReadCount();
        return(<RenderContent items={selection.article.text} />);
    };

    const renderAvatar = () =>
    {
        if (Validate.isEmpty(selection.article.author.avatarName))
        {
            return(<><PersonIcon className={classes.avatar} /></>);
        }

        const avatarUrl = AVATARS_PATH + selection.article.author.avatarName;
        return(<><img className={classes.avatar} src={avatarUrl} alt="" /></>);
    };

    return (
        <section>
            <Container className={classes.container}>
                <Box py={12}>
                    <div data-aos="fade-down">
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
                        <Divider className={classes.dividerTop} />
                        <Grid container spacing={3}>
                            <Grid item xs={1}>
                                {renderAvatar()}
                            </Grid>
                            <Grid item xs={11}>
                                <Typography className={classes.aliasName} component="p" variant="subtitle1" align="left">
                                    {selection.article.author.aliasName}
                                </Typography>
                            </Grid>
                        </Grid>
                        <Box mt={1} mb={5}>
                            <Typography component="p" variant="subtitle1">
                                Published at: {FormatDateTime(selection.article.createdAt)}
                            </Typography>
                            <Typography component="p" variant="subtitle2" color="textSecondary">
                                Updated at: {FormatDateTime(selection.article.updatedAt)}
                            </Typography>
                        </Box>
                    </div>
                    <div data-aos="fade-up">
                        {renderContent()}
                    </div>
                    <Divider className={classes.dividerBottom} />
                    <Typography component="p" variant="subtitle2">
                        Votes: {selection.article.likeCount}
                    </Typography>
                </Box>
            </Container>
        </section>
    );
}
