function T = Train(TrainPath)
% Description: This function reshapes all 2D images of the training person
% images into 1D column vectors. Then, it puts these 1D column vectors in a row to 
% construct 2D matrix 'T'.
%  
% 
% Argument:     TrainPath      - Path of the training person images 
%
% Returns:      T               - A 2D matrix, containing all 1D image vectors.
%                                 Suppose all M images in the training path 
%                                 have the same size of NxN. So the length of 1D 
%                                 column vectors is N^2 and 'T' will be a N^2xM 2D matrix.

no_folder=size(dir([TrainPath,'\*']),1)-size(dir([TrainPath,'\*m']),1)-2;
stk = '1';
stk = strcat('\',stk,'\*jpg');
folder_content = dir ([TrainPath,stk]);
nface = size (folder_content,1);
T = [];
for j = 1 :  nface
    str = int2str(j);
    str = strcat('\',str,'.jpg');
    str = strcat('\','1',str);
    str = strcat(TrainPath,str);
    img = imread(str);
    img = FaceDetection(img) ;
    img = imresize(img,[100 100]); 
    img = rgb2gray(img);
    [irow icol] = size(img);
    temp = reshape(img',irow*icol,1);
    T = [T temp];
end
end


