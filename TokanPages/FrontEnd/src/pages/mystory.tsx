import React from "react";

import HorizontalNav1 from "../components/horizontal-navs/HorizontalNav";
import StructureContainer from "../components/structures/StructureContainer";
import Content from "../components/content/Content";
import Footers from "../components/Footers";

export default function Mystory() 
{
  
  return (    
    <React.Fragment>     
      <HorizontalNav1 content={null} />
      <StructureContainer columns={[<Content content={null} />, <Footers />]} />
    </React.Fragment>
  );

}
