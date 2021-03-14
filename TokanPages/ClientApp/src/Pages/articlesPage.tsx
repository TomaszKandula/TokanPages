import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import ArticleList from "../Components/Articles/articleList";
import ArticleDetail from "../Components/Articles/articleDetail";
import { getNavigationContent } from "../Api/Services";
import { navigationDefault } from "../Api/Defaults";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function ArticlesPage() 
{
    const queryParam = useQuery();
    const id = queryParam.get("id");
    const content = id 
        ? <ArticleDetail id={id} /> 
        : <ArticleList />;

    const mountedRef = React.useRef(true);
    const [navigation, setNavigationContent] = React.useState(navigationDefault);
    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNavigationContent(await getNavigationContent());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, 
    [ updateContent ]);

    return (
        <>
            <Navigation content={navigation.content} />
            <Container>
                {content}
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
