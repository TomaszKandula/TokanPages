import * as React from "react";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, Grid, IconButton, Typography } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import axios from "axios";
import Validate from "validate.js";
import useStyles from "./Hooks/styleArticleDetail";
import { ApplicationState } from "../../Redux/applicationState";
import CenteredCircularLoader from "../../Shared/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../Shared/ContentRender/renderContent";
import { ITextObject } from "../../Shared/ContentRender/Model/textModel";
import { ARTICLE_URL } from "../../Shared/constants";
import { FormatDateTime } from "../../Shared/helpers";
import { UpdateArticle } from "../../Api/Services/articles";

export interface IArticleDetail
{
    id: string;
}

export default function ArticleDetail(props: IArticleDetail) 
{
    const classes = useStyles();
    const articleUrl = ARTICLE_URL.replace("{ID}", props.id);
    const [ article, setArticle ] = React.useState<ITextObject>({ items: [] });
    const details = useSelector((state: ApplicationState) => state.selectArticle);

    const fetchArticle = React.useCallback( async () => 
    {
        const response = await axios.get<ITextObject>(
        articleUrl, 
        {
            method: "GET", 
            responseType: "json"
        });

        setArticle(response.data);
    }, 
    [ articleUrl ]);
    
    React.useEffect(() => 
    { 
        fetchArticle() 
    }, 
    [ article.items.length, fetchArticle ] );

    const updateReadCount = async () => 
    {
        await UpdateArticle(
        {
            id: props.id,
            readCount: details.readCount + 1
        });
    };

    const renderContent = () =>
    {
        if (Validate.isEmpty(article.items))
        {
            return(<CenteredCircularLoader />);
        }
        else
        {
            updateReadCount();
            return(<RenderContent items={article.items} />);
        }
    };

    return (
        <section>
            <Container className={classes.container}>
                <Box py={12}>
                    <Link to="/articles">
                        <IconButton>
                            <ArrowBack/>
                        </IconButton>
                    </Link> 
                    <Divider className={classes.divider} />
                    <Box mb={5}>
                        <Grid container spacing={3}>
                            <Grid item xs={6}>
                                <Typography component="p" variant="subtitle1">
                                    Published at: {FormatDateTime(details.createdAt)}
                                </Typography>
                                <Typography component="p" variant="subtitle2" color="textSecondary">
                                    Updated at: {FormatDateTime(details.updatedAt)}
                                </Typography>
                            </Grid>
                            <Grid item xs={6}>
                                <Typography component="p" variant="subtitle1" align="right">
                                    Read: {details.readCount}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Box>
                    {renderContent()}
                </Box>
            </Container>
        </section>
    );
}
