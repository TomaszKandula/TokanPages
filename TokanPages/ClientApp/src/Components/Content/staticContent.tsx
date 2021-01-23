import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, IconButton } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import axios from "axios";
import Validate from "validate.js";
import useStyles from "./styleStaticContent";
import CenteredCircularLoader from "../../Shared/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../Shared/ContentRender/renderContent";
import { ITextObject } from "../../Shared/ContentRender/Model/textModel";

interface IStoryContent
{
    dataUrl: string;
}

export default function StaticContent(props: IStoryContent) 
{

    const classes = useStyles();
    const [ data, setData ] = React.useState<ITextObject>({ items: [] });

    const fetchData = React.useCallback( async () => 
    {
        const response = await axios.get<ITextObject>(props.dataUrl, {method: "get", responseType: "json"});
        setData(response.data);
    }, [ props.dataUrl ]);
    React.useEffect( () => { fetchData() }, [ data.items.length, fetchData ] );

    return (
        <section>
            <Container className={classes.container}>
                <Box py={12}>
                    <Link to="/">
                        <IconButton>
                            <ArrowBack/>
                        </IconButton>
                    </Link> 
                    <Divider className={classes.divider} />
                    {Validate.isEmpty(data) 
                        ? <CenteredCircularLoader /> 
                        : <div className={classes.typography}>{RenderContent(data, classes.typography)}</div>}
                </Box>
            </Container>
        </section>
    );

}
