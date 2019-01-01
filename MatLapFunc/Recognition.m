function Output = Recognition(ProjectedImages_for_each_Person , Eigenfaces_for_each_Person ,  m_for_each_Person , Eigenfaces_Size , Test )
% Recognizing step....
%
% Description: This function compares two faces by projecting the images into facespace and 
% measuring the Euclidean distance between them.
%
% Argument:      m_for_each_Person               -Mean of the training
%                                                 images for each person
%                                                 concatenated in one matrix
%                                                  
%                Eigenfaces_for_each_Person      -Eigen vectors of the
%                                                 covariance matrix of the training
%                                                 images for each person concatenated in one matrix.
%
%               ProjectedImages_for_each_Person  -Projection Matrix of
%                                                 centered images into facespace images for each person 
%                                                 concatenated in one matrix
%               Eigenfaces_Size                  -Number of columns in Eigenfaces matrix for each person
%                                                 concatenated in one matrix
% 
% Returns:      Output                           -Index of matched person.            
InputImage = Test ;
[rSize  CSize] = size(m_for_each_Person);
tempStart = [];
Euc_dist = [];
Euc_dist2 = [];
tempStart = 1 ;
for j = 1 : CSize   %loop for each person
%%%%%%%%%%%%%%%%%%%%%%%% Extracting the PCA features from test image
temp = InputImage;
[irow icol] = size(temp);
InImage = reshape(temp',irow*icol,1);
Normalized = double(InImage)- m_for_each_Person(:,j) ;
End = (tempStart + Eigenfaces_Size(:,j) ) - 1;
ProjectedTestImage = Eigenfaces_for_each_Person( :  ,[tempStart : End])'*Normalized; 
%%%%%%%%%%%%%%%%%%%%%%%% Calculating Euclidean distances 
% Euclidean distances between the projected test image and the projection
% of all centered training images are calculated. Test image is
% supposed to have minimum distance with its corresponding image in the
% training database.
MINI = 100000000000000000000;
for i = tempStart : End
    q = ProjectedImages_for_each_Person([ 1 : Eigenfaces_Size(:,j) ] ,i);
    temp = ( norm( ProjectedTestImage - q ) )^2;
    if(temp(1,1) > MINI(1,1))
        MINI(1,1) = temp(1,1);
    end
    Euc_dist = [Euc_dist temp];
end
Euc_dist2 = [Euc_dist2 MINI];
tempStart = tempStart + Eigenfaces_Size(:,j) ;
end
[Euc_dist_min2 , Recognized_index2] = min(Euc_dist2);
Output = (Recognized_index2);
end

