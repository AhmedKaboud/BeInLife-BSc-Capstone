function [ Output ] = SignIn(ProjectedImages_for_each_Person , Eigenfaces_for_each_Person , m_for_each_Person , Eigenfaces_Size ,  testImagePath )
% SignIn step....
% Description: This function selects the best matched person for input image face
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
img = imread(testImagePath);
img = FaceDetection(img);
img = imresize(img,[100 100]);
img = rgb2gray(img);
Output = Recognition(ProjectedImages_for_each_Person , Eigenfaces_for_each_Person ,  m_for_each_Person , Eigenfaces_Size , img );
end