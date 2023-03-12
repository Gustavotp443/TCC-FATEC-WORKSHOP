import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import * as P from '../pages/Index';
const Routering = () => {
  return (
    <Router>
        <Routes>
            <Route path="/" element={<P.Home/>}/>
            <Route path="/signup" element={<P.SingUp/>}/>
            <Route path="*" element={<P.Error/>}/>
        </Routes>
    </Router>
  )
}

export default Routering

