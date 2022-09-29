import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { GetNavigationContentAction } from "../../Store/Actions";
import { GetFooterContentAction } from "../../Store/Actions";
import { ProgressOnScroll } from "../../Shared/Components";
import { Navigation } from "../../Components/Layout";
import { Footer } from "../../Components/Layout";
import { ArticleList } from "../../Components/Articles";
import { ArticleDetail } from "../../Components/Articles";
import { Colours } from "../../Theme";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export const ArticlesPage = (): JSX.Element => 
{
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id");

    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.getNavigationContent());
        dispatch(GetFooterContentAction.getFooterContent());
    }, 
    [ dispatch, language?.id ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            {id ? <ProgressOnScroll height={10} bgcolor={Colours.application.navigation} duration={1} /> : null}
            <Container>
                {id ? <ArticleDetail id={id} /> : <ArticleList />}
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
