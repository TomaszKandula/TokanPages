import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, IconButton } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import axios from "axios";
import Validate from "validate.js";
import useStyles from "./Hooks/styleArticleDetail";
import CenteredCircularLoader from "../../Shared/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../Shared/ContentRender/renderContent";
import { ITextObject } from "../../Shared/ContentRender/Model/textModel";

interface IArticleDetail
{
    uid: string;
}

export default function ArticleDetail(props: IArticleDetail) 
{

    const classes = useStyles();
    const content = 
    {
        footprint: "https://maindbstorage.blob.core.windows.net/tokanpages/content/articles/{UID}/text.json"
    };

    const [ article, setArticle ] = React.useState<ITextObject>({ items: [] });
    const articleUrl = content.footprint.replace("{UID}", props.uid);
    const fetchArticle = React.useCallback( async () => 
    {
        const response = await axios.get<ITextObject>(articleUrl, {method: "get", responseType: "json"});
        setArticle(response.data);    
    }, [ articleUrl ]);
    React.useEffect( () => { fetchArticle() }, [ article.items.length, fetchArticle ] );

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
                    {Validate.isEmpty(article.items) 
                        ? <CenteredCircularLoader /> 
                        : <RenderContent items={article.items} />}
                </Box>
            </Container>
        </section>
    );
}
