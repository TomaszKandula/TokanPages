import { ContentPageDataState } from "../../../../../Store/States";

export interface GetVerifiedComponentsProps {
    components: string[];
    state: ContentPageDataState;
    languageId: string;
}
