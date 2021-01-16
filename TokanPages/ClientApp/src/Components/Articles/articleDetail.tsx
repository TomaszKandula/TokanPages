import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Card, CardMedia, Divider, IconButton } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ReactHtmlParser from 'react-html-parser';
import axios from "axios";
import useStyles from "./Hooks/styleArticleDetail";
import Validate from "validate.js";
import CenteredCircularLoader from "Shared/ProgressBar/centeredCircularLoader";
import SyntaxHighlighter from "react-syntax-highlighter";
import { Languages } from "../../Shared/languageList";

interface IArticleDetail
{
    uid: string;    
}

interface ITextObject
{
    items: ITextItem[];
}

interface ITextItem
{
    id: string,
    type: string;
    value: string;
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

    const renderText = (props: ITextItem) =>
    {
        return(
            <div key={props.id}>
                <Typography variant="body1" component="span" className={classes.typography}>
                    {ReactHtmlParser(props.value)}
                </Typography>
            </div>
        );
    }

    const renderImage = (props: ITextItem) =>
    {
        return(
            <div key={props.id}>
                <Card elevation={2}>
                    <CardMedia component="img" image={props.value} alt="" />
                </Card>
            </div>
        );
    }

    const renderCode = (props: ITextItem) =>
    {
        return(
            <div key={props.id}>
                <SyntaxHighlighter language={props.type}>
                    {atob(props.value)}
                </SyntaxHighlighter>
            </div>
        );
    }

    const renderArticle = (jsonObject: ITextObject | undefined) => 
    {

        if (jsonObject === undefined)
        {
            return(<div>Cannot render article.</div>);
        }
        
        let renderBuffer: JSX.Element[] = [];
        jsonObject.items.forEach(item => 
        {
            if (item.type === "html") renderBuffer.push(renderText(item));
            if (item.type === "image") renderBuffer.push(renderImage(item));
            if (Languages.includes(item.type)) renderBuffer.push(renderCode(item)); 
        });

        return(
            <div data-aos="fade-up">
                {renderBuffer}
            </div>
        );

    }

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
                    {Validate.isEmpty(article.items) ? <CenteredCircularLoader /> : renderArticle(article)}
                </Box>
            </Container>
        </section>
    );
}
