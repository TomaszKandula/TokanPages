import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, IconButton } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import Validate from "validate.js";
import CenteredCircularLoader from "../../Shared/Components/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../Shared/Components/ContentRender/renderContent";
import { ITextObject } from "../../Shared/Components/ContentRender/Models/textModel";
import useStyles from "./staticContentStyle";
import { IApplicationState } from "../../Redux/applicationState";
import { 
    ActionCreators,
    TRequestContent, 
    REQUEST_POLICY, 
    REQUEST_STORY, 
    REQUEST_TERMS 
} from "../../Redux/Actions/getStaticContentAction";

export default function StaticContent(props: { content: TRequestContent }) 
{
    const classes = useStyles();
    const [ data, setData ] = React.useState<ITextObject>({ items: [] });

    const dispatch = useDispatch();
    const getStaticContentState = useSelector((state: IApplicationState) => state.getStaticContent);

    const fetchData = React.useCallback(async () => 
    {
        switch(props.content)
        {
            case REQUEST_STORY: 
                if (Validate.isEmpty(getStaticContentState.story.items) && !getStaticContentState.story.isLoading)
                {
                    dispatch(ActionCreators.getStoryContent());
                    return;
                }
                setData(getStaticContentState.story);
            break;

            case REQUEST_TERMS: 
                if (Validate.isEmpty(getStaticContentState.terms.items) && !getStaticContentState.terms.isLoading)
                {
                    dispatch(ActionCreators.getTermsContent());
                    return;
                }
                setData(getStaticContentState.terms);
            break;
    
            case REQUEST_POLICY: 
                if (Validate.isEmpty(getStaticContentState.policy.items) && !getStaticContentState.policy.isLoading)
                {
                    dispatch(ActionCreators.getPolicyContent());
                    return;
                }
                setData(getStaticContentState.policy);
            break;
        }
    }, 
    [ dispatch, getStaticContentState, props.content ]);

    React.useEffect(() => { fetchData() }, [ data.items.length, fetchData ]);

    return (
        <section>
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
                        {Validate.isEmpty(data) 
                            ? <CenteredCircularLoader /> 
                            : <RenderContent items={data.items}/>}
                    </div>
                </Box>
            </Container>
        </section>
    );
}
