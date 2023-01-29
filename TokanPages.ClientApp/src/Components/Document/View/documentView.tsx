import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, IconButton } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import { DocumentStyle } from "./documentStyle";

import { 
    ContentPolicyState, 
    ContentTermsState,
    ContentStoryState 
} from "../../../Store/States";

import { 
    ProgressBar, 
    RenderContent 
} from "../../../Shared/Components";

export const DocumentView = (props: ContentPolicyState | ContentTermsState | ContentStoryState): JSX.Element => 
{
    const classes = DocumentStyle();
    return (
        <section className={classes.section}>
            <Container className={classes.container}>
                <Box py={12}>
                    <div data-aos="fade-down">
                        <Link to="/">
                            <IconButton>
                                <ArrowBack/>
                            </IconButton>
                        </Link> 
                        <Divider className={classes.divider} />
                    </div>                    
                    <div data-aos="fade-up">
                        {props.isLoading 
                            ? <ProgressBar /> 
                            : <RenderContent items={props.content?.items}/>}
                    </div>
                </Box>
            </Container>
        </section>
    );
}
